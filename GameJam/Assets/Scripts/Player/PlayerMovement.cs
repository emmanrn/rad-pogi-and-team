using UnityEngine;

public class PlayerMovement
{
    private CharacterController cc;
    private Transform transform;
    private float speed;
    private float gravity;
    private Vector3 velocity;
    public void Init(CharacterController cc, Transform transform, Vector3 velocity, float speed, float gravity)
    {
        this.cc = cc;
        this.transform = transform;
        this.velocity = velocity;
        this.speed = speed;
        this.gravity = gravity;
    }

    public void FixedTick()
    {
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        cc.Move(move * speed * Time.fixedDeltaTime);

        if (cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.fixedDeltaTime;
        cc.Move(velocity * Time.fixedDeltaTime);
    }
}
