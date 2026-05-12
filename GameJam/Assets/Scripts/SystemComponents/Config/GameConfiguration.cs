using UnityEngine;

[System.Serializable]
public class GameConfiguration
{
    public static GameConfiguration activeConfig;
    public static string filePath => $"{FilePaths.root}gameconf.cfg";

    public string displayResolution = "1920x1080";

    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    public void Load()
    {
        var ui = ConfigMenu.Instance.ui;

        int resIndex = 0;
        for (int i = 0; i < ui.resolutions.options.Count; i++)
        {
            string resolution = ui.resolutions.options[i].text;
            if (resolution == displayResolution)
            {
                resIndex = i;
                break;
            }
        }

        ui.resolutions.value = resIndex;

        ui.masterVolume.value = musicVolume;
    }

    public void Save()
    {
        FileManager.Save(filePath, JsonUtility.ToJson(this));
    }
}
