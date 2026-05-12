using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//Holds reference to interactables and blinking state operations.
public class EnvironmentSystemManager : MonoBehaviour
{
    public static EnvironmentSystemManager Instance;
    public static UnityEvent <int, int> OnShardGet = new UnityEvent<int, int>();

    [SerializeField] private Light light;
    [SerializeField] private Color dreamColor;
    [SerializeField] private Color disColor;
    private List<ShardDropper> shardDroppers;
    private PhotographComponent photograph;
    private string roomName;

    //Non-Persistent Singleton
    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        //Find References in Scene
        GetPhotoReference();
        GetShardDroppers();
        roomName = SceneManager.GetActiveScene().name;
    }

    private void GetPhotoReference()
    {
        var photo = transform.GetComponentsInChildren<PhotographComponent>().ToList();
        
        if (photo.Count == 0)
        {
            Debug.Log("No Photo found");
            return;
        }

        photograph = photo[0];
        photograph.gameObject.SetActive(false);
        
        if (photo.Count > 1)
        {
            Debug.Log($"Found More than one Instance of Photo. Removing the rest");
            for (int i = 1; i < photo.Count; i++)
            {
                Destroy(photo[i]);
            }
        }
    }

    private void GetShardDroppers()
    {
        shardDroppers = GetComponentsInChildren<ShardDropper>(includeInactive:true).ToList();
    }

    public void CheckRoomCompletion()
    {
        if (AreShardsComplete())
            EnablePhotograph();
    }

    private bool AreShardsComplete()
    {
        int count = 0;
        int total = shardDroppers.Count;
        foreach(var shardDropper in shardDroppers)
            if (shardDropper.isDropped)
                count++;
        
        OnShardGet.Invoke(count, total);
        
        return count >= total;
    }

    private void EnablePhotograph()
    {
        photograph.gameObject.SetActive(true);
    }

    public void OnRoomComplete()
    {
        StartCoroutine(OnRoomCompleteOperation());
    }

    private IEnumerator OnRoomCompleteOperation()
    {
        Debug.Log($"Completed Room {roomName}");
        
        //Do Dialogue
        
        yield return new WaitUntil(() => Player.Instance.currentState == Player.State.IsPlaying);
        LoadSceneSystem.Instance.LoadNextRoom(roomName);
    }

    public void TransformState(bool blinkState)
    {
        foreach (var shardDropper in shardDroppers)
            shardDropper.ChangeSpriteState(blinkState);
        
        //Filter
        if (!blinkState)
        {
            light.color = dreamColor; //Do Something
        }
        else
        {
            light.color = disColor;
            //Do Something
        }
    }
}