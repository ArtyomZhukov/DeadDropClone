using UnityEngine;

public class Disintegration : MonoBehaviour
{
    public float destroySpeed = 0.005f;

    SpriteRenderer spriteRenderer = null;
    TextMesh textMesh = null;

    private bool isSprite = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMesh = GetComponent<TextMesh>();
        isSprite = spriteRenderer != null;

        if (spriteRenderer == null && textMesh == null)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (isSprite)
        {
            if (spriteRenderer.color.a == 0)
            {
                Destroy(gameObject);
            }
            Color col = spriteRenderer.color;
            col.a -= destroySpeed;
            spriteRenderer.color = col;
        }
        else
        {
            if (textMesh.color.a == 0)
            {
                Destroy(gameObject);
            }
            Color col = textMesh.color;
            col.a -= destroySpeed;
            textMesh.color = col;
        }
    }
}
