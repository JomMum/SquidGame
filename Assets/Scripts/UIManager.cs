using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Drawing;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Game_1")]
    public Text LimitTimeUI_1;

    [Header("Game_2")]
    public Game2_Manager game2_manager;
    public Text LimitTimeUI_2;
    public Image dalgonaGage;

    [Header("Game_3")]
    public Game3_Manager game3_Manager;
    public Image resultUI;
    public GameObject game3Bar;
    public Image LimitTimeUI_3;
    public Transform patternsFolder;
    public GameObject patternPrefab;

    public Sprite[] keyImg;
    public Sprite[] resultImg;

    [Header("Game_4")]
    public Text LimitTimeUI_5;

    [Header("Result")]
    public Text countText;
    public Text prizeText;

    public bool activeResultUI;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game_1")
        {
            //제한시간 UI
            string timeStr = string.Format("{0:D2}", gameManager.limitTime);
            LimitTimeUI_1.text = "00 : " + timeStr;
        }
        else if (SceneManager.GetActiveScene().name == "Game_2")
        {
            //제한시간 UI
            string timeStr = string.Format("{0:D2}", gameManager.limitTime);
            LimitTimeUI_2.text = "00 : " + timeStr;

            //달고나 포인트
            dalgonaGage.fillAmount = (float)game2_manager.currentTargetIndex / (game2_manager.predefinedPoints.Count + 1);
        }
        else if (SceneManager.GetActiveScene().name == "Game_3")
        {
            // 점수 범위
            float maxPoint = 10f;
            float minBarPosition = 380f;
            float maxBarPosition = -380f;

            // 줄다리기 상태 표시
            float mappedPosition = Mathf.Lerp(minBarPosition, maxBarPosition, game3_Manager.tugOfWarPoint / maxPoint);
            MoveBar(mappedPosition);

            //결과창 UI
            ShowResultUI();

            //제한시간 UI
            LimitTimeUI_3.fillAmount = (float)game3_Manager.limitTime / (float)game3_Manager.maxLimitTime;
        }
        else if (SceneManager.GetActiveScene().name == "Game_4")
        {
            int min = gameManager.limitTime / 60;
            string minStr = string.Format("{0:D2}", min);
            int sec = gameManager.limitTime - (60 * min);
            string secStr = string.Format("{0:D2}", sec);

            LimitTimeUI_5.text = minStr + " : " + secStr;
        }
        else if (SceneManager.GetActiveScene().name == "Result")
        {
            countText.text = ResultManager.Instance.tryCount + "";

            int prizeCount = 456 - ResultManager.Instance.tryCount;
            string prizeStr = prizeCount + "00000000";
            prizeText.text = long.Parse(prizeStr).ToString("N0");
        }
    }

    void MoveBar(float targetPosition)
    {
        if (game3Bar.transform.localPosition.x != targetPosition)
        {
            game3Bar.transform.localPosition = Vector3.MoveTowards(game3Bar.transform.localPosition, new Vector3(targetPosition, 0,0), Time.deltaTime * 100f);
        }
        else
        {
            // 시간에 따라 X 축으로 흔들림
            float shakeOffset = Mathf.Sin(Time.time * 50) * 3f;
            game3Bar.transform.localPosition += new Vector3(shakeOffset, 0, 0);
        }
    }

    public void ShowPatternUI()
    {
        foreach (Transform child in patternsFolder.transform)
        {
            Destroy(child.gameObject); // 자식 오브젝트 삭제
        }

        for (int i = 0; i < game3_Manager.pattern.Length; i++)
        {
            //패턴 UI 표시
            if (game3_Manager.pattern[i] != 0)
            {
                GameObject pattern = Instantiate(patternPrefab, patternsFolder);
                pattern.GetComponent<Image>().sprite = keyImg[game3_Manager.pattern[i] - 1];
            }
        }
    }

    public void ChangePatternColor(int patternCount)
    {
        GameObject pattern = patternsFolder.transform.GetChild(patternCount - 1).gameObject;
        pattern.GetComponent<Image>().color = new UnityEngine.Color(288 / 255f, 93 / 255f, 93 / 255f);
    } 

    void ShowResultUI()
    {
        if (!activeResultUI) return;
        activeResultUI = false;

        if (gameManager.isGameStart)
        {
            resultUI.gameObject.transform.parent.gameObject.SetActive(false);
            resultUI.gameObject.transform.parent.gameObject.SetActive(true);

            if (game3_Manager.isClear)
                resultUI.sprite = resultImg[0];
            else
                resultUI.sprite = resultImg[1];
        }
    }

    void DisableResultUI()
    {
        resultUI.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
