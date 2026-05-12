using UnityEngine;

public class ShardDropper : MonoBehaviour
{
    public bool isDropped = false;

    public void DropShard()
    {
        isDropped = true;
        EnvironmentSystemManager.Instance.CheckRoomCompletion();
    }
}