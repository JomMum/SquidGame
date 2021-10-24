using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game3_Manager : MonoBehaviour
{
    public GameManager gameManager;

    public int tugOfWarPoint = 5; //줄다리기 포인트
    public int limitTime; //제한시간
    public int maxLimitTime = 50; //최대 제한시간

    public int[] pattern = new int[15]; //입력 패턴
    int curPattern; //현재 패턴

    public bool canTouch; //터치 가능한 상태인가
    public bool isClear; //해당 패턴을 클리어했는가

    bool isOnce;

    void Update()
    {
        if(gameManager.isGameStart)
        {
            if(!isOnce)
            {
                WaveSetting();
                isOnce = true;
            }

            //게임 승패 판정
            if (tugOfWarPoint >= 10) //성공
                SceneManager.LoadScene("Game_5");
            else if (tugOfWarPoint <= 0) //실패
                SceneManager.LoadScene("MainMenu");

            if (canTouch)
            {
                //실패/통과 유무 판정
                if (pattern[curPattern] == 0)
                {
                    canTouch = false;
                    isClear = true;
                }
                else if ((Input.GetKeyUp(KeyCode.LeftArrow) && pattern[curPattern] != 1)
                         || (Input.GetKeyUp(KeyCode.RightArrow) && pattern[curPattern] != 2)
                         || (Input.GetKeyUp(KeyCode.UpArrow) && pattern[curPattern] != 3)
                         || (Input.GetKeyUp(KeyCode.DownArrow) && pattern[curPattern] != 4))
                {
                    canTouch = false;
                    isClear = false;
                }

                //올바르게 키를 눌렀을 시
                if ((Input.GetKeyUp(KeyCode.LeftArrow) && pattern[curPattern] == 1)
                    || (Input.GetKeyUp(KeyCode.RightArrow) && pattern[curPattern] == 2)
                    || (Input.GetKeyUp(KeyCode.UpArrow) && pattern[curPattern] == 3)
                    || (Input.GetKeyUp(KeyCode.DownArrow) && pattern[curPattern] == 4))
                {
                    curPattern++;
                }
            }
        }

    }

    void WaveSetting()
    {
        curPattern = 0;
        SetPattern();

        canTouch = true;
        StartCoroutine(StartLimitTime());
    }

    void SetPattern()
    {
        int patternLength = Random.Range(5, 11); //패턴 개수 결정

        //랜덤한 패턴 코드 저장
        for (int i=0; i<pattern.Length; i++)
        {
            if (i < patternLength)
            {
                int patternCode = Random.Range(1, 5);
                pattern[i] = patternCode;
            }
            else
                pattern[i] = 0;
        }
    }

    IEnumerator StartLimitTime()
    {
        for(limitTime = maxLimitTime; limitTime > 0; limitTime--)
            yield return new WaitForSeconds(0.1f);

        canTouch = false;

        //성공 판정
        if (isClear)
        {
            tugOfWarPoint++;
        }
        else
        {
            tugOfWarPoint--;
        }

        isClear = false;
        WaveSetting();
    }
}
