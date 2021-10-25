using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NeedleScript : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject needleMark;
    public GameObject needleMarks;
    public GameObject fadeout;
    public GameObject[] dalgona;

    public int moveSpd;
    public int dalgonaPoint = 0;
    public int targetPoint;

    bool isGameOver;
    bool isGameClear;

    bool isPointUp; //점수가 오를 수 있는가 (올바른 선)
    bool isOnce;

    void Update()
    {
        if (gameManager.isGameStart)
        {
            if (isGameClear)
            {
                //성공
                FadeManager fadeMgr = fadeout.GetComponent<FadeManager>();
                fadeMgr.isGameClear = true;

                Invoke("FadeOut", 1.5f);
                isGameClear = false;
            }
            else if (isGameOver)
            {
                //게임오버
                PointManager.Instance.challengeNum++;
                PointManager.Instance.cashPrize--;

                Invoke("FadeOut", 1.5f);
                isGameOver = false;
            }

            if (!isOnce)
            {
                dalgona[Random.Range(0,3)].gameObject.SetActive(true);
                targetPoint = 30;
                isOnce = true;
            }

            //바늘 이동
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            transform.Translate(new Vector2(h * moveSpd, v * moveSpd) * Time.deltaTime);

            //일정 점수 도달 시 성공
            if (dalgonaPoint >= targetPoint)
            {
                isGameClear = true;
            }
            else if(gameManager.timeOver)
            {
                isGameOver = true;
                gameManager.timeOver = false;
            }

            //스페이스바 뗐을 시 변수 false
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                //점수 증가
                if (isPointUp)
                {
                    dalgonaPoint++;
                    isPointUp = false;
                }

                //바늘 흔적 생성
                GameObject mark = Instantiate(needleMark);
                mark.transform.parent = needleMarks.gameObject.transform;
                mark.transform.localPosition = new Vector2(gameObject.transform.localPosition.x - 130, gameObject.transform.localPosition.y - 130);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (gameManager.isGameStart)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //올바른 선에 맞춰 클릭했을 시
                if (collision.gameObject.CompareTag("DalgonaLine"))
                    isPointUp = true;
            }
        }
    }

    void FadeOut()
    {
        fadeout.SetActive(true);
    }
}
