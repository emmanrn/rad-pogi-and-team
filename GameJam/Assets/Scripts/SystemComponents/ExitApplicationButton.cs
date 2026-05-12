using System;
using UnityEngine;
using UnityEngine.UI;

public class ExitApplicationButton : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Application.Quit());
    }
}