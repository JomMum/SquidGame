using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Script : MonoBehaviour
{
    public bool isback=true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(turn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator turn()
    {
        yield return new WaitForSeconds(1.5f);

        Debug.Log("돌았다");
        isback = true;

        yield return new WaitForSeconds(2);
        Debug.Log("뒤돌아봄");
       isback = false;

        StartCoroutine(turn());
    }
}
