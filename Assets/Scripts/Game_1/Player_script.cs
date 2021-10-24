using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_script : MonoBehaviour
{
    Animator anim;
    public GameManager gameManager;
    public Bot_Script Bot_Script;
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
                SceneManager.LoadScene("Game_2");
                isGameClear = false;
            }
            else if (isGameOver)
            {
                //게임오버
                isWalk = false;
                anim.SetTrigger("doDie");
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
                        transform.Translate(new Vector2(0, movespeed));
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
}
