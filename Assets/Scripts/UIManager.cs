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
    public Image resultUI;
    public Image LimitTimeUI_3;
    public GameObject patternUI;
    public GameObject patterns;

    public Sprite[] keyImg;
    public Sprite[] resultImg;

    bool isShow;

    [Header("Game_5")]
    public Text LimitTimeUI_5;

    [Header("ResultScene")]
    public Text ChallengeText;
    public Text PrizeText;

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
            TargetPointUI_2.text = "SCORE " + needleScipt.dalgonaPoint + " / " + needleScipt.targetPoint;
        }
        else if(SceneManager.GetActiveScene().name == "Game_3")
        {
            if (game3_Manager.curPattern == 0)
                isShow = false;

            if(isShow)
            {
                if(game3_Manager.curPattern != 0)
                {
                    Image curPattern = patterns.transform.GetChild(game3_Manager.curPattern - 1).gameObject.GetComponent<Image>();
                    curPattern.color = new Color(255 / 255f, 139 / 255f, 139 / 255f);
                }
            }

            pointUI.text = "SCORE " + game3_Manager.tugOfWarPoint + " / " + "10";

            //패턴 UI
            if (game3_Manager.canTouch && !isShow)
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
        else if(SceneManager.GetActiveScene().name == "ResultScene")
        {
            ChallengeText.text = PointManager.Instance.challengeNum.ToString();

            int num1 = PointManager.Instance.cashPrize / 10;
            int num2 = PointManager.Instance.cashPrize % 10;
            PrizeText.text = num1 + "," + num2 + "00,000,000";
        }
    }

    void ShowPatternUI()
    {
        for (int i = 0; i < patterns.transform.childCount; i++)
        {
            Destroy(patterns.transform.GetChild(i).gameObject);
        }

        isShow = true;

        for (int i = 0; i < game3_Manager.pattern.Length; i++)
        {
            if (game3_Manager.pattern[i] != 0)
            {
                //패턴 UI 표시
                GameObject pattern = Instantiate(patternUI);
                pattern.transform.parent = patterns.transform;

                Image patternSpr = pattern.gameObject.GetComponent<Image>();
                patternSpr.sprite = keyImg[game3_Manager.pattern[i] - 1];
            }
            else
                return;
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
