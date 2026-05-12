using UnityEngine;

public class PlayerCamera
{
    private float sensitivity;
    private float xRotation;
    private Transform transform;
    public void Init(float sensitivity, float xRotation, Transform transform)
    {
        this.sensitivity = sensitivity;
        this.xRotation = xRotation;
        this.transform = transform;
    }

    public void Tick()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
