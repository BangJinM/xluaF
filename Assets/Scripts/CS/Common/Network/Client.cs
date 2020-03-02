using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using XLua;
namespace Networks
{
    [LuaCallCSharp]
    public enum NetWorkState
    {
        [Description("initial state")]
        CLOSED,

        [Description("connecting server")]
        CONNECTING,

        [Description("server connected")]
        CONNECTED,

        [Description("disconnected with server")]
        DISCONNECTED,

        [Description("connect timeout")]
        TIMEOUT,

        [Description("netwrok error")]
        ERROR
    }
    [LuaCallCSharp]
    public class Client
    {
        public string ip;
        public int port;
        public NetworkStateChange networkStateChangeEvent = null;
    
        private Socket socket = null;
        private NetWorkState netWorkState;
        private Transport transport = null;

        public Client(string ip, int port) {
            this.ip = ip;
            this.port = port;
        }

        public void Connect() {
            IPAddress ipAddress = null;
            try
            {
                IPAddress[] addresses = Dns.GetHostEntry(ip).AddressList;
                foreach (var item in addresses)
                {
                    if (item.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = item;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                NetWorkChanged(NetWorkState.ERROR);
                return;
            }

            if (ipAddress == null)
            {
                throw new Exception("can not parse host : " + ip);
            }

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ie = new IPEndPoint(ipAddress, port);

            socket.BeginConnect(ie, new AsyncCallback((result) =>
            {
                try
                {
                    this.socket.EndConnect(result);
                    transport = new Transport(this.socket, ExcuteLuaHandler, Dispose);
                    transport.receive();
                    NetWorkChanged(NetWorkState.CONNECTING);
                }
                catch (SocketException e)
                {
                    if (netWorkState != NetWorkState.TIMEOUT)
                    {
                        NetWorkChanged(NetWorkState.ERROR);
                    }
                    Dispose();
                }
            }), this.socket);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                this.socket.Shutdown(SocketShutdown.Both);
                this.socket.Close();
                this.socket = null;
            }
            catch (Exception)
            {
                //todo : 有待确定这里是否会出现异常，这里是参考之前官方github上pull request。emptyMsg
            }
        }

        /// <summary>
        /// 网络状态变化
        /// </summary>
        /// <param name="state"></param>
        private void NetWorkChanged(NetWorkState state)
        {
            netWorkState = state;

            if (networkStateChangeEvent != null)
            {
                networkStateChangeEvent(state);
            }
        }

        /// <summary>
        /// 注册客户端状态改变事件
        /// </summary>
        /// <param name="networkStateChange"></param>
        public void setNeworkChangedEventListener(NetworkStateChange networkStateChange) {
            networkStateChangeEvent = networkStateChange;
        }

        /// <summary>
        /// 收到消息回调
        /// </summary>
        /// <param name="message"></param>
        internal void ExcuteLuaHandler(SocketModel message)
        {
            Handlers handlers = Handlers.GetHandlers();
            handlers.ExcuteLuaHandler( message);
        }

        /// <summary>
        /// 注册lua时间
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="action"></param>
        public void RegisterLuaHandler(int messageType, LuaHandlerAction action)
        {
            Handlers handlers = Handlers.GetHandlers();
            handlers.RegisterLuaHandler(messageType, action);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public void SendMsg(int type, int area, int command, string message)
        {
            SocketModel sm = new SocketModel(type, area, command, message);
            this.transport.send(sm);
        }
    }
}