using UnityEngine;

public class FilePaths
{
    private const string HOME_DIRECTORY_SYMBOL = "~/";

    public static string root = $"{runtimePath}/gameData/";

    public static string runtimePath
    {
        get
        {
#if UNITY_EDITOR
            return "Assets/appdata/";
#else
            return Application.persistentDataPath + "/appdata/";
#endif
        }
    }
}
