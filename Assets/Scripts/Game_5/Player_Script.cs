using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    bool isleft;
    bool isright;
    float curY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isleft = false;
            isright = false;

            curY = transform.position.y;
            isleft = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isleft = false;
            isright = false;

            curY = transform.position.y;
            isright = true;
        }

        if (isleft)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-1, curY + 2), 0.1f);
        }
        else if (isright)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(1, curY + 2), 0.1f);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Glass"))
        {
            if (transform.position.y >= curY + 2)
            {
                Glass_script glassScript = collision.gameObject.GetComponent<Glass_script>();

                if(glassScript.ishard)
                {
                    Debug.Log("성공");
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
