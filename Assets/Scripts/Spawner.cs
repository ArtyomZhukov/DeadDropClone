using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public List<SpawnObject> SpawnObjects = new List<SpawnObject>();

    void Start()
    {
        foreach (var Obj in SpawnObjects)
        {
            for (int i = 0; i < Obj.Count; i++)
            {
                Object obj = Instantiate(Obj.Object, GetSpawnCoordinate(), Quaternion.identity);

                Character character = ((GameObject)obj).GetComponent<Bot>();
                if (character != null)
                {
                    SceneManager.Bots.Add(character);
                }
                else
                {
                    character = ((GameObject)obj).GetComponent<Player>();
                    if (character != null)
                    {
                        SceneManager.PlayerSpy = character;
                    }
                }
            }
        }
    }

    private Vector3 GetSpawnCoordinate()
    {
        Vector3 range = transform.localScale / 2.0f;
        return transform.position + new Vector3(
            Random.Range(-range.x, range.x), Random.Range(-range.y, range.y), 0);
    }
}

[System.Serializable]
public class SpawnObject
{
    [Range(0, 100)] public int Count;
    public Object Object;
}
