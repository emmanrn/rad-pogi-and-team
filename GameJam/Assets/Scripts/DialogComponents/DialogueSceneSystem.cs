// using System;
// using DialogComponents;
// using UnityEngine;
//
// public enum DialogSceneType { Intro, Outro }
// public class DialogSceneSystem : MonoBehaviour
// {
//     public static DialogSceneSystem Instance;
//     public string sceneType;
//     private DialogLoader _dialogLoader;
//
//     private void Awake()
//     {
//         if (Instance == null)
//             Instance = this;
//         
//         _dialogLoader = GetComponent<DialogLoader>();
//     }
//
//     public void StartDialogScene()
//     {
//         _dialogLoader.LoadDialog();
//     }
// }