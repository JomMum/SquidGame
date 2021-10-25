using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    GameManager gameManager;
    GameObject fadeout;
    public GameObject player;

    Animator anim;

    public int moveSpd;

    public bool isWalk;
    bool isleft;
    bool isright;
    float curY;

    bool isGameClear;

    bool isOnce;
    bool isOnceAnim;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
        fadeout = GameObject.Find("FadeWindow").transform.Find("Fadeout").gameObject;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (gameManager.isGameStart)
        {
            if (isGameClear)
            {
                //성공
                FadeManager fadeMgr = fadeout.GetComponent<FadeManager>();
                fadeMgr.isGameClear = true;

                Invoke("FadeOut", 1.5f);

                isGameClear = false;
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

                if (collision.gameObject.CompareTag("EndFloor"))
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
                            //애니메이션 실행
                            if (!glassScript.isBroken)
                            {
                                Animator glassAnim = collision.gameObject.GetComponent<Animator>();
                                glassAnim.SetTrigger("doBroken");
                                glassScript.isBroken = true;

                                Invoke("DelayFall", 0.8f);
                            }
                            else
                                anim.SetTrigger("doFall");

                            //점수 감소
                            PointManager.Instance.challengeNum++;
                            PointManager.Instance.cashPrize--;

                            //플레이어 스폰
                            GameObject newPlayer = Instantiate(player);
                            newPlayer.transform.position = new Vector2(0, -10.5f);

                            isOnceAnim = true;
                        }
                    }
                }
            }
        }
    }

    void DelayFall()
    {
        anim.SetTrigger("doFall");
    }

    void FadeOut()
    {
        fadeout.SetActive(true);
    }
}
