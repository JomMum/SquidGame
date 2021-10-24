using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    public GameManager gameManager;

    public bool isWalk;
    bool isleft;
    bool isright;
    float curY;

    bool isOnce;

    void Update()
    {
        if (gameManager.isGameStart)
        {
            if(!isOnce)
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

                    isWalk = false;
                }
                else if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    isleft = false;
                    isright = false;

                    curY = transform.position.y;
                    isright = true;

                    isWalk = false;
                }
            }

            if (isleft)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-1.153f, curY + 2), 0.1f);
            }
            else if (isright)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(1.207f, curY + 2), 0.1f);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(gameManager.isGameStart)
        {
            if (collision.gameObject.CompareTag("Glass"))
            {
                if (transform.position.y >= curY + 2)
                {
                    Glass_script glassScript = collision.gameObject.GetComponent<Glass_script>();

                    if (glassScript.ishard)
                    {
                        isWalk = true;
                    }
                    else
                    {
                        isWalk = false;

                        Animator glassAnim = collision.gameObject.GetComponent<Animator>();

                        glassAnim.SetTrigger("doBroken");
                    }
                }
            }
        }
    }
}
