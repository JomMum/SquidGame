using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game_1")]
    public GameObject[] player_1;

    [Header("Game_5")]
    public GameObject[] player_5;

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
                Instantiate(player_1[PointManager.Instance.playerNum - 1]);
                limitTime = 40; //제한시간 40초
                StartCoroutine("StartTimer");
                isOnce = true;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Game_2")
        {
            if (isGameStart && !isOnce)
            {
                limitTime = 30; //제한시간 30초
                StartCoroutine("StartTimer");
                isOnce = true;
            }
        }
        else if(SceneManager.GetActiveScene().name == "Game_5")
        {
            if (isGameStart && !isOnce)
            {
                Instantiate(player_5[PointManager.Instance.playerNum - 1]);
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
