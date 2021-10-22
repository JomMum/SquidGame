using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Script : MonoBehaviour
{
    Animator anim;
    public GameManager gameManager;
    public bool isback=true;
    bool isTurn;
    bool isOnce;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(gameManager.isGameStart && !isOnce)
        {
            StartCoroutine(turn());
            isOnce = true;
        }
    }

    IEnumerator turn()
    {
        //뒤 보는 중
        isTurn = true;
        isback = true;
        yield return new WaitForSeconds(1.5f);

        //돌아봄
        if (isTurn)
        {
            isTurn = false;
            anim.SetTrigger("doTurn");
        }
        yield return new WaitForSeconds(1);

        //바라보는 중
        isback = false;
        yield return new WaitForSeconds(1);
        anim.SetTrigger("doReturn");

        StartCoroutine(turn());
    }
}
