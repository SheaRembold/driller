using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour
{
    public Font buttonFont;
    public GameObject main;
    public GameObject help;
    public GameObject load;
    GUIStyle buttonStyle = null;
    GUIStyle loadStyle = null;
    bool inHelp;
    bool loading;

    void Start()
    {
        inHelp = false;
        loading = false;
    }

    void Update()
    {

    }

    void OnGUI()
    {
        if (loading)
        {
            if (loadStyle == null)
            {
                loadStyle = new GUIStyle();
                loadStyle.font = buttonFont;
                loadStyle.fontSize = Screen.height / 20;
                loadStyle.alignment = TextAnchor.MiddleCenter;
            }
            GUI.Label(new Rect((Screen.width - 100) / 2f, (Screen.height - 100) / 2f, 100, 100), "Loading...", loadStyle);
        }
        else
        {
            int width = Screen.width / 4;
            int height = Screen.height / 10;
            if (buttonStyle == null)
            {
                buttonStyle = new GUIStyle(GUI.skin.button);
                buttonStyle.font = buttonFont;
            }
            buttonStyle.fontSize = height / 2;

            if (inHelp)
            {
                if (GUI.Button(new Rect((Screen.width - width) / 2f, height * 8, width, height), "Back", buttonStyle))
                {
                    inHelp = false;
                    main.SetActive(true);
                    help.SetActive(false);
                }
            }
            else
            {
                if (GUI.Button(new Rect((Screen.width - width) / 2f, height * 6, width, height), "Play", buttonStyle))
                {
                    Application.LoadLevel("Main");
                    loading = true;
                    load.SetActive(true);
                    main.SetActive(false);
                }
                if (GUI.Button(new Rect((Screen.width - width) / 2f, height * 8, width, height), "Help", buttonStyle))
                {
                    inHelp = true;
                    help.SetActive(true);
                    main.SetActive(false);
                }
            }
        }
    }
}
