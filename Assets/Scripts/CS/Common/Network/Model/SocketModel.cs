using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;//注意要用到这个dll
namespace Networks
{
    [ProtoContract]
    public class SocketModel
    {
        [ProtoMember(1)]
        private int type = -1;//消息类型 登录/游戏
        [ProtoMember(2)]
        private int area = -1;//消息区域码
        [ProtoMember(3)]
        private int command = -1;//指令
        [ProtoMember(4)]
        private string message;//消息
        public SocketModel()
        {

        }
        public SocketModel(int type, int area, int command, string message)
        {
            this.type = type;
            this.area = area;
            this.command = command;
            this.message = message;
        }
        public new int GetType()
        {
            return type;
        }
        public void SetType(int type)
        {
            this.type = type;
        }
        public int GetArea()
        {
            return this.area;
        }
        public void SetArea(int area)
        {
            this.area = area;
        }
        public int GetCommand()
        {
            return this.command;
        }
        public void SetCommand(int command)
        {
            this.command = command;
        }
        public string GetMessage()
        {
            return message;
        }
        public void SetMessage(string message)
        {
            this.message = message;
        }
    }
}