using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;

    public Color colour = Color.white;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = (Screen.height * 2 / 100);

        GUIStyle style = new GUIStyle();
        //Screen.height - h
        Rect rect = new Rect(300, 0, w, h);
        style.alignment = TextAnchor.LowerLeft;
        style.fontSize = h ;
        style.normal.textColor = colour;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}