using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileOperation 
{
    public static byte[] ReadFileBytes(string filePath ) {
        try
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            if (!File.Exists(filePath))
            {
                return null;
            }

            File.SetAttributes(filePath, FileAttributes.Normal);
            return File.ReadAllBytes(filePath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("ReadFileBytes failed! path = {0} with err = {1}", filePath, ex.Message));
            return null;
        }
    }
}
