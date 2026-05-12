using System;
using TMPro;
using UnityEngine;

public class MemoryShardCount : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI txt;
    private void Awake()
    {
        txt.gameObject.SetActive(false);
    }

    public void ChangeText(int currCount, int totalCount)
    {
        txt.gameObject.SetActive(true);
        txt.text = $"Memory Shard: {currCount}/{totalCount}";
    }
    private void OnEnable()
    {
       EnvironmentSystemManager.OnShardGet.AddListener(ChangeText);
    }

    private void OnDisable()
    {
        EnvironmentSystemManager.OnShardGet.RemoveListener(ChangeText);
    }
}
