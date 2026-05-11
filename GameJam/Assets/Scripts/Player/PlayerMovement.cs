using UnityEngine;

public class PlayerMovement
{
    private CharacterController cc;
    private Transform transform;
    private float speed;
    private float gravity;
    private Vector3 velocity = Vector3.zero;
    public void Init(CharacterController cc, Transform transform, float speed, float gravity)
    {
        this.cc = cc;
        this.transform = transform;
        this.speed = speed;
        this.gravity = gravity;
    }

    public void Tick()
    {
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        move = Vector3.ClampMagnitude(move, 1f);
        cc.Move(move * speed * Time.deltaTime);

        if (cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}
