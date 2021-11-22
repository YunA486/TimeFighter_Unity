using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HumanStage : MonoBehaviour
{

    public GameObject UIOptionImg;
    public GameManager gameManager;
    // ��ġ��
    private int HumanPoint = 0;

    public void Awake()
    {
        ScoreManager score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        HumanPoint = score.stagePoint;
    }

    // ȭ�� �� ��ư ���

    public void Retry()
    {
        ScoreManager score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        score.stagePoint = 0;

        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Next()
    {
        // ��ġ��
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    // �ɼ� ȭ��
    public void option()
    {
        Time.timeScale = 0;
        UIOptionImg.SetActive(true);
    }
    public void close()
    {
        Time.timeScale = 1;
        UIOptionImg.SetActive(false);
    }

    // �ɼ� ��ư ���
    public void restart()
    {
        ScoreManager score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        score.stagePoint = HumanPoint;


        // ��ġ��
        Time.timeScale = 1;
        SceneManager.LoadScene(11);
    }

}
