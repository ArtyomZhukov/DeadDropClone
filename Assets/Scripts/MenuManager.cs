using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject SpyRecords = null;
    [SerializeField] private GameObject SniperRecords = null;
    [SerializeField] private GameObject RecordPrafab = null;


    void Start()
    {
        SceneManager.pause = false;
        SceneManager.endGame = false;
        FillRecords(SpyRecords, SaveManager.recordsData.spyRecords);
        FillRecords(SniperRecords, SaveManager.recordsData.sniperRecords);
    }

    private void FillRecords(GameObject parent, List<RecordsData.Record> data)
    {
        int i = 1;
        foreach (var record in data)
        {
            GameObject rec = (Instantiate(RecordPrafab, Vector3.zero, Quaternion.identity) as GameObject);
            rec.transform.SetParent(parent.transform);
            rec.transform.localScale = Vector3.one;
            rec.GetComponent<RecordRow>().SetValues(i++.ToString(), record.name, record.time);
        }
    }

    public static void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Level1");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
