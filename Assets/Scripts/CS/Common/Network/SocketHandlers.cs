using System.Collections.Generic;
using XLua;
namespace Networks { 

    /// <summary>
    /// 网络回调句柄管理
    /// </summary>
    public class Handlers
    {
        static Handlers staticHandlers;
        Dictionary<int, LuaHandlerAction> handles;
        Handlers()
        {
            handles = new Dictionary<int, LuaHandlerAction>();
        }
        public static Handlers GetHandlers()
        {
            if (staticHandlers == null)
            {
                staticHandlers = new Handlers();
            }
            return staticHandlers;
        }

        public void RegisterLuaHandler(int messageType, LuaHandlerAction action)
        {
            if (handles.ContainsKey(messageType))
            {
                return;
            }
            handles.Add(messageType, action);
        }

        public void ExcuteLuaHandler(int messageType, string str)
        {
            LuaHandlerAction action = handles[messageType];
            action("1", str);
        }
    }
}