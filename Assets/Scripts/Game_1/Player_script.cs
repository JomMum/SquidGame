using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_script : MonoBehaviour
{
    public Bot_Script Bot_Script;
    public float movespeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            if (Bot_Script.isback)
                transform.Translate(new Vector2(0, movespeed));
            else
                Debug.Log("탈락");
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("FinalLine"))
        {
            Debug.Log("성공");
        }
    }
}
