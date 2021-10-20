using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game3_UIManager : MonoBehaviour
{
    public Game3_Manager gameManager;
    public Text patternText;
    public Text resultUI;
    public Image LimitTimeUI;

    string patternName;

    void Update()
    {
        //패턴 UI
        if (gameManager.canTouch)
            ShowPatternUI();

        //결과창 UI
        ShowResultUI();

        //제한시간 UI
        ShowLimitTimeUI();
    }

    void ShowPatternUI()
    {
        patternText.text = "";

        for (int i = 0; i < gameManager.pattern.Length; i++)
        {
            //패턴 UI 표시
            switch (gameManager.pattern[i])
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
        if (!gameManager.canTouch)
        {
            if (gameManager.isClear)
                resultUI.text = "성공!";
            else
                resultUI.text = "실패!";
        }
        else
        {
            resultUI.text = "";
        }
    }

    void ShowLimitTimeUI()
    {
        LimitTimeUI.fillAmount = (float)gameManager.limitTime / (float)gameManager.maxLimitTime;
    }
}
