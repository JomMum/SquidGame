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

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(gameManager.isGameStart)
        {
            if (Input.anyKey)
            {
                if (Bot_Script.isback)
                {
                    anim.SetBool("isWalk", true);
                    transform.Translate(new Vector2(0, movespeed));
                }
                else
                {
                    //게임오버
                    SceneManager.LoadScene("MainMenu");
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
                //성공
                SceneManager.LoadScene("Game_2");
            }
        }
    }
}
