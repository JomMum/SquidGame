using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleScript : MonoBehaviour
{
    public int moveSpd;

    void Update()
    {
        int randomX = Random.Range(230, -230);
        int randomY = Random.Range(340, -135);

        Vector2.MoveTowards(transform.localPosition, new Vector2(randomX, randomY), moveSpd);
    }
}
