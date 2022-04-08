using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XLua;

namespace Games
{
    class LuaEnvironment
    {
        public const string luaScriptsFolder = "Game/LuaScript/";
        private static LuaEnv luaEnv;
        public static LuaEnv LuaEnv
        {
            get
            {
                if (luaEnv == null)
                {
                    luaEnv = new LuaEnv();
                }

                return luaEnv;
            }
        }

        public static void Dispose()
        {
            if (luaEnv != null)
            {
                luaEnv.Dispose();
                luaEnv = null;
            }
        }

        private static IEnumerator DoTick()
        {
            while (true)
            {
                try
                {
                    luaEnv.Tick();
                }
                catch (Exception e)
                {
                }
            }
        }

        public static void InitLuaEnv(LuaEnv luaEnv)
        {
            InitLoader(luaEnv);
            InitGlobal(luaEnv);
        }

        static void InitLoader(LuaEnv luaEnv)
        {
            luaEnv.AddLoader(CustomLoader);
        }

        static byte[] CustomLoader(ref string filepath)
        {
            string scriptPath = string.Empty;
#if UNITY_EDITOR
            {
                scriptPath = Path.Combine(Application.dataPath, luaScriptsFolder);
                scriptPath = Path.Combine(scriptPath, filepath);
                scriptPath = scriptPath.Replace("/", "\\");
                Debug.Log("CsutomLoader: " + scriptPath);
                if (File.Exists(scriptPath))
                {
                    return File.ReadAllBytes(scriptPath);
                }
                else
                {
                    return null;
                }
            }
#else
            return null;
#endif
        }

        static void InitGlobal(LuaEnv luaEnv)
        {
            LuaTable global = luaEnv.NewTable();
            luaEnv.Global.Set("global", global);
        }
    }
}
