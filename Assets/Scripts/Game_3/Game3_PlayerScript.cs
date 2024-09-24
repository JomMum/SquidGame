using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3_PlayerScript : MonoBehaviour
{
    public bool isTeam1;
    public bool isPlayer;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        if (isPlayer)
        {
            anim.runtimeAnimatorController = ResultManager.Instance.playerAnim[ResultManager.Instance.characterIndex];
        }

        if (isTeam1)
            anim.SetTrigger("isTeam1Pull");
        else
            anim.SetTrigger("isTeam2Pull");
    }
}
