using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Text SpyReadyText = null;
    [SerializeField] private Text SniperReadyText = null;

    private bool spyReady = false;
    private bool sniperReady = false;

    void Update()
    {
        if (Input.GetButtonDown("Space"))
        {
            ChangeReady(spyReady = !spyReady, SpyReadyText);
        }
        if (Input.GetButtonDown("Shoot"))
        {
            ChangeReady(sniperReady = !sniperReady, SniperReadyText);
        }
        if (spyReady && sniperReady)
        {
            MenuManager.Play();
        }
    }

    private void ChangeReady(bool ready, Text text)
    {
        text.text = ready ? "Ready" : "Not ready";
        text.color = ready ? Color.green : Color.red;
    }
}
