using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject fadeout;

    Animator anim;

    public int moveSpd;

    public bool isWalk;
    bool isleft;
    bool isright;
    float curY;

    bool isGameOver;
    bool isGameClear;

    bool isOnce;
    bool isOnceAnim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (gameManager.isGameStart)
        {
            if (isGameClear)
            {
                //성공
                isGameClear = false;
            }
            else if (isGameOver)
            {
                //게임오버
                Invoke("FadeOut", 1.5f);
                isGameOver = false;
            }

            if (!isOnce)
            {
                isWalk = true;
                isOnce = true;
            }

            if (isWalk)
            {
                if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    isleft = false;
                    isright = false;

                    curY = transform.position.y;
                    isleft = true;

                    anim.SetBool("isJump", true);

                    isWalk = false;
                }
                else if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    isleft = false;
                    isright = false;

                    curY = transform.position.y;
                    isright = true;

                    anim.SetBool("isJump", true);

                    isWalk = false;
                }
            }

            if (isleft)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-1.153f, curY + 2), moveSpd * Time.deltaTime);
            }
            else if (isright)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(1.207f, curY + 2), moveSpd * Time.deltaTime);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(gameManager.isGameStart)
        {
            if (transform.position.y >= curY + 2)
            {
                anim.SetBool("isJump", false);

                if (collision.gameObject.CompareTag("FinalLine"))
                {
                    isGameClear = true;
                }
                else if (collision.gameObject.CompareTag("Glass"))
                {
                    Glass_script glassScript = collision.gameObject.GetComponent<Glass_script>();

                    if (glassScript.ishard)
                    {
                        isWalk = true;
                    }
                    else
                    {
                        isWalk = false;

                        if(!isOnceAnim)
                        {
                            Animator glassAnim = collision.gameObject.GetComponent<Animator>();
                            glassAnim.SetTrigger("doBroken");

                            anim.SetTrigger("doFall");

                            isOnceAnim = true;
                        }

                        isGameOver = true;
                    }
                }
            }
        }
    }

    void FadeOut()
    {
        fadeout.SetActive(true);
    }
}
