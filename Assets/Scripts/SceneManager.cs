using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private float roundTime = 180;
    [SerializeField] private float exitTime = 10;

    [SerializeField] private int objectivesCount = 5;
    [SerializeField] private List<GameObject> saveZone = null;

    [SerializeField] private Text objectivesText = null;
    [SerializeField] private Text timerText = null;
    [SerializeField] private Text winnerText = null;
    [SerializeField] private GameObject pausePanel = null;
    [SerializeField] private GameObject recordPanel = null;
    [SerializeField] private Text recordNameText = null;

    [SerializeField] private GameObject blackStart = null;
    [SerializeField] private GameObject blackEnd = null;
    [SerializeField] private GameObject mask = null;

    public static Vector3 playerDeadPosition = new Vector3(1, 1, 1337);

    private float currentRoundTime;
    private static int objectivesDone = 0;
    private static int lastObjective = 0;
    private static bool saveZoneOpenTime = false;

    public static bool endGame = false;
    public static bool pause = false;

    public static Player winner = Player.None;

    public static List<Character> Bots = new List<Character>();
    public static Character PlayerSpy = null;

    public enum Player { Sniper, Spy, None };

    void Start()
    {
        objectivesDone = lastObjective = 0;
        winner = Player.None;
        pause = endGame = false;
        currentRoundTime = roundTime;
        ShowObjectives();
        blackStart.SetActive(true);
    }

    void Update()
    {
        if (pause)
            return;

        if (!endGame)
        {
            Timer();

            if (lastObjective < objectivesDone)
            {
                ShowObjectives();
            }
            if (saveZoneOpenTime)
            {
                saveZone[Random.Range(0, 4)].SetActive(true);
                saveZoneOpenTime = false;
            }
            if (objectivesDone >= objectivesCount)
            {
                winner = Player.Spy;
            }
            if (currentRoundTime <= 0)
            {
                winner = Player.Sniper;
            }
            if (winner != Player.None)
            {
                GameOver();
            }
            if (Input.GetButtonDown("Escape"))
            {
                Pause(true);
            }
        }
        else
        {
            if (exitTime > 0)
            {
                exitTime -= Time.deltaTime;
                return;
            }
            pause = true;
            if (SaveManager.CheckRecord(winner == Player.Spy, (int)roundTime - (int)currentRoundTime))
            {
                recordPanel.SetActive(true);
            }
            else
            {
                Menu();
            }
        }
    }

    public void NewRecord()
    {
        if (recordNameText.text != "")
        {
            string name = recordNameText.text;
            if (name.Length > 10)
            {
                name = name.Remove(10, name.Length - 10);
            }
            SaveManager.NewRecord(winner == Player.Spy, name, (int)roundTime - (int)currentRoundTime);
            Menu();
        }
    }

    private void GameOver()
    {
        endGame = true;
        winnerText.gameObject.SetActive(true);
        blackEnd.SetActive(true);
        foreach (var bot in Bots)
        {
            (bot as Bot).SetBlackSkin();
        }

        if (playerDeadPosition.z == 1337)
        {
            mask.transform.SetParent(PlayerSpy.transform);
            mask.transform.localPosition = Vector3.zero;
        }
        else
        {
            mask.transform.position = playerDeadPosition;
        }

        winnerText.text = (winner == Player.Sniper ? "SNIPER" : "SPY") + " WINS!";
    }

    public void Pause(bool active)
    {
        pausePanel.SetActive(active);
        pause = active;
    }

    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    private void Timer()
    {
        timerText.text = SecondsToTime(currentRoundTime);
        currentRoundTime -= Time.deltaTime;
    }

    private void ShowObjectives()
    {
        lastObjective = objectivesDone;
        objectivesText.text = $"{objectivesDone}/{objectivesCount} OBJECTIVES";
    }

    private static bool openedOnce = false;
    public static void IsSaveZoneOpenTime()
    {
        if (objectivesDone == 4 && !openedOnce)
        {
            saveZoneOpenTime = openedOnce = true;
        }
    }

    public static void ObjectiveDone()
    {
        objectivesDone++;
    }

    public static string SecondsToTime(float sec)
    {
        int seconds = (int)sec % 60;
        if (sec > 180)
            return "3:00";
        return (int)sec / 60 + ":" + (seconds < 10 ? "0" + seconds : seconds + "");
    }
}
