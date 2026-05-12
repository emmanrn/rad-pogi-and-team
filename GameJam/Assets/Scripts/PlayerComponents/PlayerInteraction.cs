using UnityEngine;

public class PlayerInteraction
{
    private Camera mainCamera;
    private LayerMask interactableMask;
    private float interactDistance;
    private GameObject prompt;

    public void Init(
        Camera mainCamera,
        LayerMask interactableMask,
        GameObject prompt,
        float interactDistance)
    {
        this.mainCamera = mainCamera;
        this.prompt = prompt;
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
        {
            prompt.SetActive(false);
            return;
        }

        prompt.SetActive(true);

        if (hit.collider.TryGetComponent(
            out IInteractable interactable))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact();
            }
        }
    }
}