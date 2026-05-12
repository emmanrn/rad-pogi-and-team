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
        if (currentRoom == RoomType.Kitchen)
            StartCoroutine(LoadScene(RoomType.LivingRoom));
        else if (currentRoom == RoomType.LivingRoom)
            StartCoroutine(LoadScene(RoomType.PlayRoom));
        else if (currentRoom == RoomType.PlayRoom)
        {
           StartCoroutine(LoadScene("EndingScene"));
        }
    }
    
    //basic loadingsystem. can turn into async operation with loading screen
    public IEnumerator LoadScene(string sceneName)
    { 
       int i = SceneUtility.GetBuildIndexByScenePath(sceneName);
       if (i <= -1)
       {
               Debug.Log("Empty Scene");
               yield break;
       }
       
       //If player exists, set to loading
       if (Player.Instance != null && Player.Instance.isPLayerInitialized) 
           Player.Instance.SetState(Player.State.IsLoading);
       
       //Load Scene and wait
       SceneManager.LoadScene(sceneName);
       yield return new WaitUntil(() => SceneManager.GetActiveScene().isLoaded);
       
       //If player exist. Reinitialize
       if (Player.Instance != null && Player.Instance.isPLayerInitialized)
       {
           Player.Instance.InitializePlayerController();
           
           yield return new WaitUntil(() => Player.Instance.isPLayerInitialized);
           Player.Instance.SetState(Player.State.IsPlaying);
       }
    }
}

public static class RoomType
{
    public const string Kitchen = "KitchenScene";
    public const string LivingRoom = "LivingRoomScene";
    public const string PlayRoom = "PlayRoomScene";
    
}