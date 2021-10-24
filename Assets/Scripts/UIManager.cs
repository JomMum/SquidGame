using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Game_1")]
    public Text LimitTimeUI_1;

    [Header("Game_2")]
    public NeedleScript needleScipt;
    public Text LimitTimeUI_2;
    public Text TargetPointUI_2;

    [Header("Game_3")]
    public Game3_Manager game3_Manager;
    public Text pointUI;
    public Text patternText;
    public Image resultUI;
    public Image LimitTimeUI_3;

    public Sprite[] resultImg;

    [Header("Game_5")]
    public Text LimitTimeUI_5;

    string patternName;

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Game_1")
        {
            //제한시간 UI
            LimitTimeUI_1.text = "00 : " + gameManager.limitTime;
        }
        else if(SceneManager.GetActiveScene().name == "Game_2")
        {
            //제한시간 UI
            LimitTimeUI_2.text = "00 : " + gameManager.limitTime;

            //달고나 포인트
            TargetPointUI_2.text = "점수: " + needleScipt.dalgonaPoint + " / " + needleScipt.targetPoint;
        }
        else if(SceneManager.GetActiveScene().name == "Game_3")
        {
            pointUI.text = "점수: " + game3_Manager.tugOfWarPoint + " / " + "10";

            //패턴 UI
            if (game3_Manager.canTouch)
                ShowPatternUI();

            //결과창 UI
            ShowResultUI();

            //제한시간 UI
            LimitTimeUI_3.fillAmount = (float)game3_Manager.limitTime / (float)game3_Manager.maxLimitTime;
        }
        else if(SceneManager.GetActiveScene().name == "Game_5")
        {
            int min = gameManager.limitTime / 60;
            int sec = gameManager.limitTime - (60 * min);

            LimitTimeUI_5.text = min + " : " + sec;
        }
    }

    void ShowPatternUI()
    {
        patternText.text = "";

        for (int i = 0; i < game3_Manager.pattern.Length; i++)
        {
            //패턴 UI 표시
            switch (game3_Manager.pattern[i])
            {
                case 1:
                    patternName = "좌";
                    break;
                case 2:
                    patternName = "우";
                    break;
                case 3:
                    patternName = "상";
                    break;
                case 4:
                    patternName = "하";
                    break;
                default:
                    patternName = "";
                    break;
            }

            patternText.text += patternName;
        }
    }

    void ShowResultUI()
    {
        if (!game3_Manager.canTouch)
        {
            resultUI.gameObject.transform.parent.gameObject.SetActive(true);

            if (game3_Manager.isClear)
                resultUI.sprite = resultImg[0];
            else
                resultUI.sprite = resultImg[1];
        }
        else
        {
            resultUI.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
