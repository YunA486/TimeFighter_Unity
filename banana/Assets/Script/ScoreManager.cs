using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    public int stagePoint;

    private void Awake()
    {
        var objs = FindObjectsOfType<ScoreManager>();
        if(objs.Length == 1)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private GUIStyle guiStyle = new GUIStyle(); //create a new variable

    //private void OnGUI()
    //{

    //    guiStyle.fontSize = 10; //change the font size
    //    GUILayout.Label("Score : " + stagePoint.ToString(), guiStyle);
    //}

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));


        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        guiStyle.fontSize = 35; //change the font size
        GUILayout.Label("Score : " + stagePoint.ToString(), guiStyle);

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
}