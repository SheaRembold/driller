using UnityEngine;
using System.Collections;

public class GUIStuff : MonoBehaviour
{
    public float timeLength = 60;
    float timeRemain;
    Rect timeRect;
    Rect winnerRect;
    Rect[] playerRects;
    Rect playerRect;
    bool wait;
    bool winner;
    public MapManager map;
    string winnerText = string.Empty;
    int driller = 0;
    public GUIStyle style;

    void Start()
    {
        timeRemain = timeLength;
        timeRect = new Rect(0, 0, Screen.width, 100);
        winnerRect = new Rect(0, Screen.height / 2f, Screen.width, 100);
        playerRects = new Rect[2];
        playerRects[0] = new Rect(0, Screen.height / 2f, Screen.width / 2f, 100);
        playerRects[1] = new Rect(Screen.width / 2f, Screen.height / 2f, Screen.width / 2f, 100);
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
        GUI.Label(timeRect, ((int)timeRemain).ToString(), style);
        if (winnerText != string.Empty)
            GUI.Label(winnerRect, winnerText, style);
        if (wait)
            GUI.Label(playerRect, "Waiting", style);
    }

    public void Winner(int index)
    {
        if (winner)
            return;

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
        timeRemain = timeLength;
        winner = false;
    }

    public void startWait(int pi)
    {
        playerRect = playerRects[pi];
        wait = true;
    }

    public void endWait()
    {
        wait = false;
    }
}
