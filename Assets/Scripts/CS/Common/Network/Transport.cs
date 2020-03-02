using System.Collections;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using XLua;

namespace Networks
{
    public enum TransportState
    {
        readHead = 1,       // on read head
        readBody = 2,       // on read body
        closed = 3          // connection closed, will ignore all the message and wait for clean up
    }
    class StateObject
    {
        public const int BufferSize = 1024 * 8 * 4;
        internal byte[] buffer = new byte[BufferSize];
    }
    public class Transport
    {
        public const int HeadLength = 4;

        private Socket socket;
        private Action<SocketModel> messageProcesser;

        //Used for get message
        private StateObject stateObject = new StateObject();
        private TransportState transportState;
        private IAsyncResult asyncReceive;
        private IAsyncResult asyncSend;
        internal Action onDisconnect = null;
        private  int len = 0;

        public Transport(Socket socket, Action<SocketModel> action, Action dispose)
        {
            this.socket = socket;
            messageProcesser = action;
            onDisconnect = dispose;
            transportState = TransportState.readHead;
        }


        public void send(SocketModel socketModel)
        {
            if (this.transportState != TransportState.closed)
            {
                byte[] msg = SocketUtils.Serial(socketModel);
                //消息体结构：消息体长度+消息体
                byte[] data = new byte[4 + msg.Length];
                SocketUtils.IntToBytes(msg.Length).CopyTo(data, 0);
                msg.CopyTo(data, 4);
                if (socket != null && socket.Connected)
                    this.asyncSend = socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(sendCallback), socket);
                else
                    onDisconnect();

            }
        }

        private void sendCallback(IAsyncResult asyncSend)
        {
            try
            {
                if (this.transportState == TransportState.closed) return;
                socket.EndSend(asyncSend);
            }
                catch (System.Net.Sockets.SocketException)
            {
                close();
            }
        }

        public void receive()
        {
            this.asyncReceive = socket.BeginReceive(stateObject.buffer, 0, stateObject.buffer.Length, SocketFlags.None, new AsyncCallback(endReceive), stateObject);
        }

        internal void close()
        {
            this.transportState = TransportState.closed;
            if (this.onDisconnect != null)
                this.onDisconnect();
        }

        private void endReceive(IAsyncResult asyncReceive)
        {
            if (this.transportState == TransportState.closed)
                return;
            StateObject state = (StateObject)asyncReceive.AsyncState;
            Socket socket = this.socket;

            try
            {
                int length = socket.EndReceive(asyncReceive);

                if (length > 0)
                {
                    processBytes(state.buffer, 0, length);
                    //Receive next message
                    if (this.transportState != TransportState.closed) receive();
                }
                else
                {
                    close();
                }

            }
            catch (System.Net.Sockets.SocketException)
            {
                close();
            }
        }

        internal void processBytes(byte[] bytes, int offset, int limit)
        {
            if (this.transportState == TransportState.readHead)
            {
                readHead(bytes, offset, limit);
            }
            else if (this.transportState == TransportState.readBody)
            {
                readBoby(bytes, offset, limit);
            }
        }

        public void readHead(byte[] bytes, int offset, int limit) {
            byte[] lenByte = new byte[HeadLength];
            System.Array.Copy(bytes, lenByte, HeadLength);
            len = SocketUtils.BytesToInt(lenByte, 0);
            this.transportState = TransportState.readBody;
            if (len + HeadLength <= limit)
                readBoby(bytes, offset, limit);
        }

        public void readBoby(byte[] bytes, int offset, int limit) {
            byte[] msgByte = new byte[len];
            System.Array.ConstrainedCopy(bytes, HeadLength, msgByte, 0, len);
            len = 0;
            SocketModel message = SocketUtils.DeSerial(msgByte);
            this.transportState = TransportState.readHead;
            if (messageProcesser != null)
                messageProcesser(message);
        }
    }
}