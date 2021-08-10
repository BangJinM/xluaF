
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using XLua;

namespace Games.Editors
{
    public static class LuaGenConfig
    {
        [LuaCallCSharp]
        public static List<Type> LuaGenConfig_lua_call_cs_list = new List<Type>()
        {
            typeof(ResourcesViewLocator),

        };

        [CSharpCallLua]
        public static List<Type> LuaGenConfig_CSharpCallLua = new List<Type>()
        {
        };


    }
}