using System.Collections;
using System.Collections.Generic;
using XLua;
using System;
namespace Networks
{
    [LuaCallCSharp]
    public class NetworkManager
    {
        public Client client = null;
        public static void SendMessage(string str)
        {
            Handlers handlers = Handlers.GetHandlers();
            handlers.ExcuteLuaHandler(1, str);
        }
        public static void RegisterLuaHandler(int messageType, LuaHandlerAction action)
        {
            Handlers handlers = Handlers.GetHandlers();
            handlers.RegisterLuaHandler(messageType, action);
        }

        public void setClient(Client client)
        {
            this.client = client;
        }
    }
}