using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneSystem : MonoBehaviour
{
    public static LoadSceneSystem Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void LoadNextRoom(string currentRoom)
    {
        if (currentRoom == RoomType.Start)
            StartCoroutine(LoadScene(RoomType.Kitchen));
        else if (currentRoom == RoomType.Kitchen)
            StartCoroutine(LoadScene(RoomType.LivingRoom));
        else if (currentRoom == RoomType.LivingRoom)
            StartCoroutine(LoadScene(RoomType.PlayRoom));
        else if (currentRoom == RoomType.PlayRoom)
            StartCoroutine(LoadScene(RoomType.Ending));
        else if (currentRoom == RoomType.Ending)
            StartCoroutine(LoadScene(RoomType.Start));

    }

    //basic loadingsystem. can turn into async operation with loading screen
    //Chat im gonna be honest i dont know how this function works anymore. it jusrt does. dont do anything to it.
    public IEnumerator LoadScene(string sceneName)
    {
        int i = SceneUtility.GetBuildIndexByScenePath(sceneName);
        if (i <= -1)
        {
            Debug.Log("Empty Scene");
            yield break;
        }

        //If player exists, set player to Loading
        if (Player.Instance != null)
            Player.Instance.SetState(Player.State.IsLoading);

        if (GameConfiguration.activeConfig != null)
            GameConfiguration.activeConfig.Save();

        //Load Scene and wait
        SceneManager.LoadScene(sceneName);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().isLoaded);

        AudioTrack track = null;
        switch (sceneName)
        {
            case RoomType.Start:
                track = AudioManager.Instance.PlayTrack("Audio/Music/disillusion");
                AudioManager.Instance.SetActiveTrack(track);
                break;
            case RoomType.Kitchen:
                track = AudioManager.Instance.PlayTrack("Audio/Music/dreamcore_kitchen");
                AudioManager.Instance.SetActiveTrack(track);
                break;
            case RoomType.LivingRoom:
                track = AudioManager.Instance.PlayTrack("Audio/Music/dreamcore_living_room");
                AudioManager.Instance.SetActiveTrack(track);
                break;
            case RoomType.PlayRoom:
                track = AudioManager.Instance.PlayTrack("Audio/Music/disillusion_2");
                AudioManager.Instance.SetActiveTrack(track);
                break;
            case RoomType.Ending:
                track = AudioManager.Instance.PlayTrack("Audio/Music/disillusion");
                AudioManager.Instance.SetActiveTrack(track);
                break;
        }


        //Reinitialize Player
        if (Player.Instance != null)
            Player.Instance.InitializePlayerController();
    }
}

public static class RoomType
{
    public const string Start = "StartScene";
    public const string Kitchen = "KitchenScene";
    public const string LivingRoom = "LivingRoomScene";
    public const string PlayRoom = "PlayRoomScene";
    public const string Ending = "EndingScene";

}