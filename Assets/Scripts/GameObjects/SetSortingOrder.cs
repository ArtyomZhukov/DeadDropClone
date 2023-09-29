using UnityEngine;

public class SetSortingOrder : MonoBehaviour
{
    [SerializeField] private int offset = 23;
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int sortingOrder = (int)(transform.position.y * -100) - offset;

        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = sortingOrder;
        }
        else
        {
            GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        }
        
    }
}
