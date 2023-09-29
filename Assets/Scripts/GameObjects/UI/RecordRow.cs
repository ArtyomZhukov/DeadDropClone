using UnityEngine;
using UnityEngine.UI;

public class RecordRow : MonoBehaviour
{
    [SerializeField] private Text NumberText = null;
    [SerializeField] private Text NameText = null;
    [SerializeField] private Text TimeText = null;

    public void SetValues(string number, string name, int time)
    {
        NumberText.text = number;
        NameText.text = name;
        TimeText.text = SceneManager.SecondsToTime(time);
    }
}
