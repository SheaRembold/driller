using UnityEngine;
using System.Collections;

public class GUIStuff : MonoBehaviour
{
    float timeRemain;
    Rect timeRect;
    Rect winnerRect;
    bool winner;
    public MapManager map;
    string winnerText = string.Empty;
    int driller = 0;
    public GUIStyle style;

    void Start()
    {
        timeRemain = 60;
        timeRect = new Rect(Screen.width / 2f, 0, Screen.width / 2f, 100);
        winnerRect = new Rect(Screen.width / 2f, Screen.height / 2f, Screen.width / 2f, 100);
        winner = false;
    }

    void Update()
    {
        timeRemain -= Time.deltaTime;
        if (timeRemain <= 0)
        {
            timeRemain = 0;
            if (!winner)
            {
                winner = true;
                StartCoroutine(showWinner("Player " + (driller + 1) + " survived"));
            }
        }
    }

    void OnGUI()
    {
        GUI.Label(timeRect, timeRemain.ToString(), style);
        if (winnerText != string.Empty)
            GUI.Label(winnerRect, winnerText, style);
    }

    public void Winner(int index)
    {
        winner = true;
        StartCoroutine(showWinner("Player " + (index + 1) + " caught Player " + ((index + 1) % 2 + 1)));
    }

    IEnumerator showWinner(string text)
    {
        winnerText = text;

        yield return new WaitForSeconds(3);

        winnerText = string.Empty;
        map.reset();
        driller = (driller + 1) % 2;
        winner = false;
    }
}
