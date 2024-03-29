﻿/*
 * MIT License
 *
 * Copyright (c) 2018 Clark Yang
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in 
 * the Software without restriction, including without limitation the rights to 
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
 * of the Software, and to permit persons to whom the Software is furnished to do so, 
 * subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all 
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE 
 * SOFTWARE.
 */

using System;
using System.IO;
using UnityEngine;
using System.Globalization;
using XLua;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Games
{
    public class Launcher : MonoBehaviour
    {
        private LuaTable scriptEnv;
        private LuaTable metatable;
        private Action<MonoBehaviour> onAwake;
        private Action<MonoBehaviour> onEnable;
        private Action<MonoBehaviour> onDisable;
        private Action<MonoBehaviour> onStart;
        private Action<MonoBehaviour> onDestroy;

        void Awake()
        {
            InitLuaEnv();

            if (onAwake != null)
                onAwake(this);

        }

        void InitLuaEnv()
        {
            var luaEnv = LuaEnvironment.LuaEnv;
            LuaEnvironment.InitLuaEnv(luaEnv);
            luaEnv.DoString("require(\"main.lua\")");

            var loader = US.AssetBundleLoader.Load("assets/game/Res.ab", US.LoaderMode.Async, (bool isOk, object resultObject) => {
                AssetBundle assetBundle = resultObject as AssetBundle;
            });

            var loader2 = US.ByteAssetLoader.Load("assets/game/res.ab", US.LoaderMode.Async, (bool isOk, object resultObject) => {
                var tem21p = resultObject;
            });

            var temp = US.DispatchMessageController.Instance;

            //if (result.Length != 1 )
            //    throw new Exception();

            //metatable = (LuaTable)result[0];

            //onAwake = metatable.Get<Action<MonoBehaviour>>("awake");
            //onEnable = metatable.Get<Action<MonoBehaviour>>("enable");
            //onDisable = metatable.Get<Action<MonoBehaviour>>("disable");
            //onStart = metatable.Get<Action<MonoBehaviour>>("start");
            //onDestroy = metatable.Get<Action<MonoBehaviour>>("destroy");
        }

        void OnEnable()
        {
            if (onEnable != null)
                onEnable(this);
        }

        void OnDisable()
        {
            if (onDisable != null)
                onDisable(this);
        }

        void Start()
        {
            if (onStart != null)
                onStart(this);
        }

        void OnDestroy()
        {
            if (onDestroy != null)
                onDestroy(this);

            onDestroy = null;
            onStart = null;
            onEnable = null;
            onDisable = null;
            onAwake = null;

            if (metatable != null)
            {
                metatable.Dispose();
                metatable = null;
            }

            if (scriptEnv != null)
            {
                scriptEnv.Dispose();
                scriptEnv = null;
            }
        }
    }
}