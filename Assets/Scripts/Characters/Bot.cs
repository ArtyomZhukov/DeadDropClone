using UnityEngine;

public class Bot : Character
{
    [SerializeField] [Range(0, 20)] float minActionTime = 1;
    [SerializeField] [Range(0, 20)] float maxActionTime = 5;

    private float actionTime = 0;
    private Vector2 currentMoveDirection = Vector2.zero;

    public override void FixedUpdate()
    {
        if (actionTime <= 0)
        {
            actionTime = Random.Range(minActionTime, maxActionTime);

            switch (Random.Range(0, 12))
            {
                case 0:
                    currentMoveDirection = Vector2.down;
                    break;
                case 1:
                    currentMoveDirection = Vector2.left;
                    break;
                case 2:
                    currentMoveDirection = Vector2.right;
                    break;
                case 3:
                    currentMoveDirection = Vector2.up;
                    break;
                case 4:
                    currentMoveDirection = Vector2.down + Vector2.right;
                    break;
                case 5:
                    currentMoveDirection = Vector2.down + Vector2.left;
                    break;
                case 6:
                    currentMoveDirection = Vector2.up + Vector2.left;
                    break;
                case 7:
                    currentMoveDirection = Vector2.up + Vector2.right;
                    break;
                default:
                    currentMoveDirection = Vector2.zero;
                    break;
            }
        }

        actionTime -= Time.deltaTime;
        moveDirection = currentMoveDirection;
        base.FixedUpdate();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        currentMoveDirection *= -1;
        actionTime = 0.3f;
    }

    public void SetBlackSkin()
    {
        if (animator)
        {
            animator.runtimeAnimatorController = skinManager.GetSkinById(0) as RuntimeAnimatorController;
        }
    }
}
