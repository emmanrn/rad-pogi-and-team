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
       
       //Load Scene and wait
       SceneManager.LoadScene(sceneName);
       yield return new WaitUntil(() => SceneManager.GetActiveScene().isLoaded);
       
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