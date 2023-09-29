using UnityEngine;

public class ObjectiveDone : MonoBehaviour
{
    [SerializeField] private string objectTag = "PlayerLegs";
    [SerializeField] private float timeToActivate = 0;
    [SerializeField] private bool disintegration = false;
    [SerializeField] private GameObject[] desintegrateObjects = null;
    [SerializeField] private GameObject objectiveComplete = null;

    private bool activateStart = false;

    void FixedUpdate()
    {
        if (activateStart)
        {
            if (timeToActivate <= 0)
            {
                if (objectiveComplete)
                {
                    Instantiate(objectiveComplete, Vector3.zero, Quaternion.identity);
                    SceneManager.IsSaveZoneOpenTime();
                }

                if (disintegration)
                {
                    foreach (var item in desintegrateObjects)
                    {
                        Disintegration disintegration = item.AddComponent<Disintegration>();
                        disintegration.destroySpeed = 0.001f;
                    }
                    gameObject.AddComponent<Disintegration>();
                }
                Destroy(this);
            }
            timeToActivate -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == objectTag && !activateStart)
        {
            SceneManager.ObjectiveDone();
            activateStart = true;
        }
    }
}
