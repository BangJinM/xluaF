using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AssetManager
{
    public static object LoadAsset(string path, Type type)
    {
        return Resources.Load(path, type);
    }
}
