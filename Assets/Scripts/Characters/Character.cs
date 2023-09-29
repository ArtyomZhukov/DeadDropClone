using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private Object deadBody = null;
    [SerializeField] protected SkinManager skinManager = null;

    protected Animator animator;
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer spriteRenderer;

    protected Vector2 moveDirection = Vector3.zero;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (skinManager.Count > 0)
        {
            animator.runtimeAnimatorController = skinManager.GetSkinById(
                Random.Range(1, skinManager.Count)) as RuntimeAnimatorController;
        }
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetLayerOrder();
    }

    public virtual void FixedUpdate()
    {
        if (SceneManager.pause)
            return;

        rigidbody2d.MovePosition(rigidbody2d.position + moveDirection.normalized * moveSpeed * Time.deltaTime);

        if (moveDirection != Vector2.zero)
        {
            animator.SetBool("Move", true);
            transform.localScale = new Vector3(moveDirection.x < 0 ? -1 : 1, 1, 1);
            SetLayerOrder();
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    private void SetLayerOrder()
    {
        spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
    }

    public void Die()
    {
        if (this is Player)
            SceneManager.playerDeadPosition = gameObject.transform.position;

        Instantiate(deadBody, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
