using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    private Spawner spawner;

    public void OnEnable()
    {
        spawner = (Spawner)target;
    }

    public override void OnInspectorGUI()
    {
        if (spawner.SpawnObjects.Count > 0)
        {
            foreach (SpawnObject item in spawner.SpawnObjects)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginHorizontal();
                item.Count = EditorGUILayout.IntField("Количество", item.Count);

                if (GUILayout.Button("Удалить", GUILayout.Width(65), GUILayout.Height(15)))
                {
                    spawner.SpawnObjects.Remove(item);
                    break;
                }
                EditorGUILayout.EndHorizontal();

                item.Object = EditorGUILayout.ObjectField("Объект", item.Object, typeof(Object), true);
                EditorGUILayout.EndVertical();
            }
        }
        else
        {
            EditorGUILayout.LabelField("Нет элементов в списке");
        }

        if (GUILayout.Button("Добавить элемент"))
        {
            spawner.SpawnObjects.Add(new SpawnObject());
        }

        if (GUI.changed)
        {
            SetObjectDirty(spawner.gameObject);
        }
    }

    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}