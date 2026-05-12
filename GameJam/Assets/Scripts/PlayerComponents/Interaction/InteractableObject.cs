using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log($"Interacted with {gameObject.name}");
    }
}