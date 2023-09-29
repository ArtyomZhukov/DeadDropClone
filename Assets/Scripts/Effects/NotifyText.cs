using UnityEngine;

public class NotifyText : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.1f;

    private Vector3 moveDir = Vector3.up;

    void Update()
    {
        transform.Translate(moveDir * Time.deltaTime * moveSpeed);
    }

    public void SetText(string text = "")
    {
        GetComponent<TextMesh>().text = text;
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Notify Layer";
    }
}
