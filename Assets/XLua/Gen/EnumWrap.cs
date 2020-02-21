#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    
    public class NetworksNetWorkStateWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Networks.NetWorkState), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Networks.NetWorkState), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Networks.NetWorkState), L, null, 7, 0, 0);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CLOSED", Networks.NetWorkState.CLOSED);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CONNECTING", Networks.NetWorkState.CONNECTING);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CONNECTED", Networks.NetWorkState.CONNECTED);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DISCONNECTED", Networks.NetWorkState.DISCONNECTED);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TIMEOUT", Networks.NetWorkState.TIMEOUT);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ERROR", Networks.NetWorkState.ERROR);
            
			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Networks.NetWorkState), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushNetworksNetWorkState(L, (Networks.NetWorkState)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {
			    if (LuaAPI.xlua_is_eq_str(L, 1, "CLOSED"))
                {
                    translator.PushNetworksNetWorkState(L, Networks.NetWorkState.CLOSED);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CONNECTING"))
                {
                    translator.PushNetworksNetWorkState(L, Networks.NetWorkState.CONNECTING);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CONNECTED"))
                {
                    translator.PushNetworksNetWorkState(L, Networks.NetWorkState.CONNECTED);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DISCONNECTED"))
                {
                    translator.PushNetworksNetWorkState(L, Networks.NetWorkState.DISCONNECTED);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TIMEOUT"))
                {
                    translator.PushNetworksNetWorkState(L, Networks.NetWorkState.TIMEOUT);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ERROR"))
                {
                    translator.PushNetworksNetWorkState(L, Networks.NetWorkState.ERROR);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Networks.NetWorkState!");
                }
            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Networks.NetWorkState! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
}