using UnityEngine;

public class Player : Character
{
    public override void FixedUpdate()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        base.FixedUpdate();
    }
}
