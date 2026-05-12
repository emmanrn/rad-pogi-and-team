using UnityEngine;

public class PlayerInteraction
{
    private Camera mainCamera;
    private LayerMask interactableMask;
    private float interactDistance;

    public void Init(
        Camera mainCamera,
        LayerMask interactableMask,
        float interactDistance)
    {
        this.mainCamera = mainCamera;
        this.interactableMask = interactableMask;
        this.interactDistance = interactDistance;
    }

    public void Tick()
    {
        Ray ray = new Ray(
            mainCamera.transform.position,
            mainCamera.transform.forward
        );

        bool hitSuccess = Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactDistance,
            interactableMask
        );

        Color rayColor = hitSuccess ? Color.green : Color.red;

        Debug.DrawRay(
            ray.origin,
            ray.direction * interactDistance,
            rayColor
        );

        if (!hitSuccess)
            return;

        if (hit.collider.TryGetComponent(
            out IInteractable interactable))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                interactable.Interact();
            }
        }
    }
}