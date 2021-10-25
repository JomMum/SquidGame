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

    public int moveSpd;
    public int dalgonaPoint = 0;
    public int targetPoint;

    bool isGameOver;
    bool isGameClear;

    int randomX;
    int randomY;
    bool isPointUp; //점수가 오를 수 있는가 (올바른 선)
    bool isCorrect; //올바르게 터치했는가 (올바른 선 & 바늘 흔적)
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
                Invoke("FadeOut", 1.5f);
                isGameOver = false;
            }

            if (!isOnce)
            {
                targetPoint = 10;
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

            //스페이스바 뗐을 시 변수 false
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                //점수 증가
                if (isPointUp)
                {
                    dalgonaPoint++;
                    isPointUp = false;
                }
                //올바른 선에 맞추지 않았을 시 게임 오버
                else if (!isCorrect)
                    Debug.Log("땡");
                //isGameOver = true;

                //바늘 흔적 생성
                GameObject mark = Instantiate(needleMark);
                mark.transform.parent = needleMarks.gameObject.transform;
                mark.transform.localPosition = new Vector2(gameObject.transform.localPosition.x - 130, gameObject.transform.localPosition.y - 130);

                isCorrect = false;
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
                {
                    isCorrect = true;
                    isPointUp = true;
                }
                else if(collision.gameObject.CompareTag("NeedleMark"))
                {
                    isCorrect = true;
                }
            }
        }
    }

    void FadeOut()
    {
        fadeout.SetActive(true);
    }
}
