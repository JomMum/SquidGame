using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleScript : MonoBehaviour
{
    public GameObject needleMark;
    public GameObject needleMarks;

    public int moveSpd;

    int dalgonaPoint = 0;

    int randomX;
    int randomY;

    bool isCorrectTouch; //올바른 선에 클릭했는가

    void Start()
    {
        StartCoroutine(RandomMove());
    }

    void Update()
    {
        //바늘의 랜덤 이동
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(randomX, randomY), moveSpd);


        //일정 점수 도달 시 성공
        if (dalgonaPoint >= 30)
            Debug.Log("성공");

        //올바른 선에 맞춰 클릭하지 않았을 시 게임 오버
        if (Input.GetMouseButtonDown(0) && !isCorrectTouch)
        {
            Debug.Log("실패");
        }
        //클릭 뗐을 시 변수 false
        else if(Input.GetMouseButtonUp(0))
        {
            //바늘 흔적 생성
            GameObject mark = Instantiate(needleMark);
            mark.transform.parent = needleMarks.gameObject.transform;
            mark.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 100);

            isCorrectTouch = false;
        }
    }

    IEnumerator RandomMove() //바늘의 좌표 설정
    {
        randomX = Random.Range(230, -230);
        randomY = Random.Range(340, -135);

        float randomTime = Random.Range(1, 3);
        yield return new WaitForSeconds(randomTime);

        StartCoroutine(RandomMove());
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
            //올바른 선에 맞춰 클릭했을 시
            if (collision.gameObject.CompareTag("DalgonaLine"))
            {
                isCorrectTouch = true;

                dalgonaPoint++;
            }
        }
    }
}
