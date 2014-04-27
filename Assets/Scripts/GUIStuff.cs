using UnityEngine;
using System.Collections;

public class GUIStuff : MonoBehaviour
{
    float timeRemain;
    Rect timeRect;

    void Start()
    {
        timeRemain = 60;
        timeRect = new Rect(Screen.width / 2f, 0, Screen.width / 2f, 100);
    }

    void Update()
    {
        timeRemain -= Time.deltaTime;
    }

    void OnGUI()
    {
        GUI.Label(timeRect, timeRemain.ToString());
    }
}
