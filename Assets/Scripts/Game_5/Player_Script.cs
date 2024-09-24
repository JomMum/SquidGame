using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    public GameObject player;
    GameManager gameManager;
    GameObject fadeout;

    Animator anim;

    public int moveSpd;

    public bool isWalk;
    bool isleft;
    bool isright;
    float curY;

    bool isFall;
    public bool isClear;

    bool isOnce;
    bool isOnceAnim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        fadeout = GameObject.Find("FadeWindow").transform.Find("Fadeout").gameObject;
    }

    void Start()
    {
        anim.runtimeAnimatorController = ResultManager.Instance.playerAnim[ResultManager.Instance.characterIndex];
    }

    void Update()
    {
        if (gameManager.isGameStart)
        {
            if (isClear)
            {
                //성공
                FadeManager fadeMgr = fadeout.GetComponent<FadeManager>();
                fadeMgr.isGameClear = true;

                Invoke("FadeOut", 0.5f);
            }
            else if (isFall)
            {
                // 낙하
                isFall = false;
                ResultManager.Instance.tryCount++;

                GameObject newPlayer = Instantiate(player);
                newPlayer.transform.position = new Vector3(0, -10.5f, 0);

                gameObject.GetComponent<Player_Script>().enabled = false;
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

    void FadeOut()
    {
        fadeout.SetActive(true);
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
                    isClear = true;
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
                            if (!glassScript.isBroken)
                            {
                                glassScript.isBroken = true;
                                Animator glassAnim = collision.gameObject.GetComponent<Animator>();
                                glassAnim.SetTrigger("doBroken");
                            }

                            anim.SetTrigger("doFall");

                            isOnceAnim = true;
                        }

                        isFall = true;
                    }
                }
            }
        }
    }
}
