using ProtoBuf;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
namespace Networks
{
    /// <summary>
    /// author:		mabangjin
    /// time:		2018/4/7 22:43:11
    /// fucntion:	
    /// </summary>
    class SocketUtils
    {
        static byte[] buffer = new byte[1024];
        public static byte[] Serial(SocketModel socketModel)//将SocketModel转化成字节数组
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize<SocketModel>(ms, socketModel);
                byte[] data = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(data, 0, data.Length);
                return data;
            }
        }

        public static SocketModel DeSerial(byte[] msg)//将字节数组转化成我们的消息类型SocketModel
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(msg, 0, msg.Length);
                ms.Position = 0;
                SocketModel socketModel = Serializer.Deserialize<SocketModel>(ms);
                return socketModel;
            }
        }
        public static int BytesToInt(byte[] data, int offset)
        {
            int num = 0;
            for (int i = offset; i < offset + 4; i++)
            {
                num <<= 8;
                num |= (data[i] & 0xff);
            }
            return num;
        }
        public static byte[] IntToBytes(int num)
        {
            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                bytes[i] = (byte)(num >> (24 - i * 8));
            }
            return bytes;
        }
    }
}