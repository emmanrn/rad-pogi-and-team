using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void LoadScene()
    {
        LoadSceneSystem.Instance.LoadNextRoom(SceneManager.GetActiveScene().name);
    }
    
    private void OnEnable()
    {
        button.onClick.AddListener(LoadScene);
    }
    
    private void OnDisable()
    {
        button.onClick.RemoveListener(LoadScene);
    }
}