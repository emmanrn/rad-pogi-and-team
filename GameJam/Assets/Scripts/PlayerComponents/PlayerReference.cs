using UnityEngine;

public class PlayerReference : MonoBehaviour
{
    //Singleton that holds session reference. Scuffed af sorry
    public static PlayerReference Instance;
    public CharacterController cc;
    public Camera cam;
    private void Awake()
    {
        Instance = this;
        
        //Only one cam per scene
        var cameras = gameObject.GetComponentsInChildren<Camera>();
        foreach(var c in cameras)
            if (c != cam)
                Destroy(c.gameObject);
    }
}