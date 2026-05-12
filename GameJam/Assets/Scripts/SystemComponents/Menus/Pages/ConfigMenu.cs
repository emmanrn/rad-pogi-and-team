using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ConfigMenu : MenuPage
{
    public static ConfigMenu Instance { get; private set; }
    public UI_ITEMS ui;
    private GameConfiguration config => GameConfiguration.activeConfig;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetAvailableResolutions();

        LoadConfig();
    }

    private void LoadConfig()
    {
        if (File.Exists(GameConfiguration.filePath))
            GameConfiguration.activeConfig = FileManager.Load<GameConfiguration>(GameConfiguration.filePath);
        else
            GameConfiguration.activeConfig = new GameConfiguration();

        GameConfiguration.activeConfig.Load();
    }

    private void OnApplicationQuit()
    {
        GameConfiguration.activeConfig.Save();
    }

    private void SetAvailableResolutions()
    {
        Resolution[] resolutions = Screen.resolutions;
        List<string> options = new List<string>();

        for (int i = resolutions.Length - 1; i >= 0; i--)
        {
            options.Add($"{resolutions[i].width}x{resolutions[i].height}");
        }

        ui.resolutions.ClearOptions();
        ui.resolutions.AddOptions(options);

    }
    public void SetMasterVolume()
    {
        config.musicVolume = ui.masterVolume.value;
        config.sfxVolume = ui.masterVolume.value;
        AudioMixerGroup musicMixer = AudioManager.Instance.musicMixer;
        AudioMixerGroup sfxMixer = AudioManager.Instance.sfxMixer;
        AnimationCurve audioFalloff = AudioManager.Instance.audioFalloffCurve;

        musicMixer.audioMixer.SetFloat(AudioManager.MUSIC_VOLUME_PARAM_NAME, audioFalloff.Evaluate(config.musicVolume));
        sfxMixer.audioMixer.SetFloat(AudioManager.SFX_VOLUME_PARAM_NAME, audioFalloff.Evaluate(config.sfxVolume));
        // AudioManager.Instance.SetMusicVolume(config.musicVolume);

    }

    [System.Serializable]
    public class UI_ITEMS
    {
        [Header("General")]
        public TMP_Dropdown resolutions;

        [Header("Audio")]
        public Slider masterVolume;
    }

    public void SetDisplayResolution()
    {
        string resolution = ui.resolutions.captionText.text;
        string[] values = resolution.Split('x');

        if (int.TryParse(values[0], out int width) && int.TryParse(values[1], out int height))
        {
            Screen.SetResolution(width, height, Screen.fullScreen);
            config.displayResolution = resolution;
        }
        else
            Debug.LogError($"Parsing error for screen resolution [{resolution}] could not be parsed into WIDTHxHEIGHT");
    }
}
