using System;
using System.IO;
using UnityEngine;

public class FileManager
{
    public static bool TryCreateDirectoryFromPath(string path)
    {
        if (Directory.Exists(path) || File.Exists(path))
            return true;

        if (path.Contains("."))
        {
            path = Path.GetDirectoryName(path);
            if (Directory.Exists(path))
                return true;
        }

        if (path == string.Empty)
            return false;

        try
        {
            Directory.CreateDirectory(path);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Could not create directory! {e} ");
            return false;
        }
    }

    public static void Save(string filePath, string JSONData)
    {
        if (!TryCreateDirectoryFromPath(filePath))
        {
            Debug.LogError($"Failed to save file {filePath}");
            return;
        }


        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(JSONData);
        sw.Close();

        Debug.Log($"Saved at {filePath}");

    }

    public static T Load<T>(string filePath)
    {
        if (File.Exists(filePath))
        {
            string JSONData = File.ReadAllLines(filePath)[0];

            return JsonUtility.FromJson<T>(JSONData);
        }
        else
        {
            Debug.LogError($"Error - File does not exist: {filePath}");
            return default(T);
        }
    }

}