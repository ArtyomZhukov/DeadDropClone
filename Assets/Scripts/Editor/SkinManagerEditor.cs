using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkinManager))]
public class SkinManagerEditor : Editor
{
    private SkinManager skinManager;

    public void OnEnable()
    {
        skinManager = (SkinManager)target;
    }

    public override void OnInspectorGUI()
    {
        if (skinManager.Skins.Count > 0)
        {
            foreach (CharacterSkin item in skinManager.Skins)
            {
                EditorGUILayout.BeginVertical("box");
                
                item.Controller = (RuntimeAnimatorController)EditorGUILayout.ObjectField("Контроллер",
                    item.Controller, typeof(RuntimeAnimatorController), true);

                if (GUILayout.Button("Удалить", GUILayout.Width(65), GUILayout.Height(15)))
                {
                    skinManager.Skins.Remove(item);
                    break;
                }
                EditorGUILayout.EndVertical();
            }
        }
        else
        {
            EditorGUILayout.LabelField("Нет элементов в списке");
        }

        if (GUILayout.Button("Добавить элемент"))
        {
            skinManager.Skins.Add(new CharacterSkin());
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(skinManager);
            //serializedBase.ApplyModifiedProperties();
        }
    }
}
