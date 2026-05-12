using System.Collections;
using UnityEngine;

public class PhotographComponent : MonoBehaviour
{
    public void ObtainPhotograph()
    {
        Debug.Log("Obtaining Photograph");
       EnvironmentSystemManager.Instance.OnRoomComplete();
    }
}