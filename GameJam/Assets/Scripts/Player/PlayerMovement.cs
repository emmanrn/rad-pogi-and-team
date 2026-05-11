using UnityEngine;

public class PlayerMovement
{
    private CharacterController cc;
    private Transform transform;
    private float speed;
    public void Init(CharacterController cc, Transform transform, float speed)
    {
        this.cc = cc;
        this.transform = transform;
        this.speed = speed;
    }

    public void FixedTick()
    {
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        cc.Move(move * speed * Time.fixedDeltaTime);
    }
}
