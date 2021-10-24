using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameStart = false;

    public int limitTime;
    public bool timeOver;
    bool isOnce;

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Game_1")
        {
            if(isGameStart && !isOnce)
            {
                limitTime = 40; //제한시간 40초
                StartCoroutine("StartTimer");
                isOnce = true;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Game_2")
        {
            if (isGameStart && !isOnce)
            {
                limitTime = 60; //제한시간 1분
                StartCoroutine("StartTimer");
                isOnce = true;
            }
        }
        else if(SceneManager.GetActiveScene().name == "Game_5")
        {
            if (isGameStart && !isOnce)
            {
                limitTime = 180; //제한시간 3분
                StartCoroutine("StartTimer");
                isOnce = true;
            }
        }
    }

    IEnumerator StartTimer()
    {
        for (; limitTime > 0; limitTime--)
            yield return new WaitForSeconds(1);

        timeOver = true;
    }
}
