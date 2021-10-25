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

    float RandomTime_1;
    float RandomTime_2;

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
        RandomTime_1 = Random.Range(0.5f, 2);
        RandomTime_2 = Random.Range(0.5f, 1.5f);

        //뒤 보는 중
        isTurn = true;
        isback = true;
        yield return new WaitForSeconds(RandomTime_1);

        //돌아봄
        if (isTurn)
        {
            isTurn = false;
            anim.SetTrigger("doTurn");
        }
        yield return new WaitForSeconds(1);

        //바라보는 중
        isback = false;
        yield return new WaitForSeconds(RandomTime_2);
        anim.SetTrigger("doReturn");

        StartCoroutine(turn());
    }
}
