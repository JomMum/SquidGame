using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_script : MonoBehaviour
{
    Animator anim;

    public GameManager gameManager;
    public Bot_Script Bot_Script;
    public GameObject fadeout;
    public float movespeed;

    bool isGameOver;
    bool isGameClear;

    bool isWalk;
    bool isOnce;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(gameManager.isGameStart)
        {
            if (isGameClear)
            {
                //성공
                isWalk = false;

                FadeManager fadeMgr = fadeout.GetComponent<FadeManager>();
                fadeMgr.isGameClear = true;

                Invoke("FadeOut", 1.5f);
                isGameClear = false;
            }
            else if (isGameOver)
            {
                //게임오버
                PointManager.Instance.challengeNum++;
                PointManager.Instance.cashPrize--;

                isWalk = false;
                anim.SetTrigger("doDie");

                Invoke("FadeOut", 1.5f);
                isGameOver = false;
            }

            if (!isOnce)
            {
                isWalk = true;
                isOnce = true;
            }

            if(gameManager.limitTime <= 0)
            {
                if(gameManager.timeOver)
                {
                    isGameOver = true;
                    gameManager.timeOver = false;
                }
            }

            if (Input.anyKey)
            {
                if(isWalk)
                {
                    if (Bot_Script.isback)
                    {
                        anim.SetBool("isWalk", true);
                        transform.Translate(new Vector2(0, movespeed * Time.deltaTime));
                    }
                    else
                    {
                        isGameOver = true;
                    }
                }
            }
            else
            {
                anim.SetBool("isWalk", false);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager.isGameStart)
        {
            if (collision.gameObject.CompareTag("FinalLine"))
            {
                isGameClear = true;
            }
        }
    }

    void FadeOut()
    {
        fadeout.SetActive(true);
    }
}
