using System.Collections;
using UnityEngine;

public class PhotographComponent : MonoBehaviour
{
    [SerializeField] private Sprite photoSprite;
    [SerializeField] private string photoName;
    public void ObtainPhotograph()
    { 
        Debug.Log("ObtainedPhotograph");
        GeneralUIImage.OnGeneralUIImageChange.Invoke(photoSprite, photoName);
        EnvironmentSystemManager.Instance.OnRoomComplete();
    }
}