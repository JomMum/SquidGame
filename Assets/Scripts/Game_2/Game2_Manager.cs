using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game2_Manager : MonoBehaviour
{
    [SerializeField] Image resultUI;
    [SerializeField] Sprite[] resultImg;
    [SerializeField] GameObject targetMark;
    [SerializeField] GameObject fadeOut;
    public LineRenderer dalgonaRenderer; // 달고나 선
    public LineRenderer needleRenderer; // 바늘 선

    float radius = 2f; // 반지름
    int segments = 100; // 세그먼트 수
    public Transform dalgonaCenter; // 달고나 중심 Transform

    int dalgonaType;

    private bool isDrawing = false;
    private int targetPositionCount = 0;

    public List<Vector3> predefinedPoints = new List<Vector3>();
    public int currentTargetIndex = 0;

    bool isClear = false;

    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        dalgonaType = Random.Range(0, 3);
        DrawDalgona();
        SetPredefinedPoints();

        Reset();
    }

    void Reset()
    {
        needleRenderer.positionCount = 0;
        targetPositionCount = 0;
        currentTargetIndex = 0;
    }

    void Update()
    {
        if (!gameManager.isGameStart) return;

        // 목표 지점 표기
        if (currentTargetIndex <= predefinedPoints.Count)
        {
            if (currentTargetIndex == predefinedPoints.Count)
            {
                targetMark.transform.position = predefinedPoints[0];
            }
            else
            {
                targetMark.transform.position = predefinedPoints[currentTargetIndex];
            }
        }

        // 시간 초과 시
        if (gameManager.limitTime <= 0)
        {
            if (gameManager.timeOver)
            {
                GameOver();
            }
        }

        if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭 시작
        {
            isDrawing = true;
            needleRenderer.positionCount = 0; // 선 초기화
            targetPositionCount = 0;
            currentTargetIndex = 0;
        }

        if (Input.GetMouseButtonUp(0)) // 마우스 좌클릭 끝
        {
            GameOver();
        }

        // 선 그리기
        if (isDrawing)
        {
            CheckSuccess(); // 성공 검사

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // 2D 평면으로 고정


            bool isInsideLine = true;
            if (dalgonaType == 0)
            {
                // 원의 중심과 마우스 위치 간의 거리 계산
                float distanceFromCenter = Vector3.Distance(mousePosition, dalgonaCenter.localPosition);
                isInsideLine = distanceFromCenter < radius - 0.13f || distanceFromCenter > radius + 0.13f;
            }
            else if (dalgonaType == 1)
            {
                isInsideLine = IsPointInTriangle(mousePosition, predefinedPoints[0], predefinedPoints[1], predefinedPoints[2]);
            }
            else if (dalgonaType == 2)
            {
                isInsideLine = IsPointInRectangle(mousePosition, predefinedPoints[0], predefinedPoints[1], predefinedPoints[2], predefinedPoints[3]);
            }


            // 마우스가 원 밖으로 나갔을 경우
            if (isInsideLine) 
            {
                GameOver();
            }
            // 선 그리기
            else
            {
                targetPositionCount++;
                needleRenderer.positionCount = targetPositionCount;
                needleRenderer.SetPosition(targetPositionCount - 1, mousePosition);
            }
        }
    }

    bool IsPointInTriangle(Vector3 point, Vector3 v1, Vector3 v2, Vector3 v3)
    {
        float d1 = LineDistance(point, v1, v2);
        float d2 = LineDistance(point, v2, v3);
        float d3 = LineDistance(point, v3, v1);

        return d1 > 0.15f && d2 > 0.15f && d3 > 0.15f;
    }

    bool IsPointInRectangle(Vector3 point, Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    {
        float d1 = LineDistance(point, v1, v2);
        float d2 = LineDistance(point, v2, v3);
        float d3 = LineDistance(point, v3, v4);
        float d4 = LineDistance(point, v4, v1);

        return d1 > 0.15f && d2 > 0.15f && d3 > 0.15f && d4 > 0.15f;
    }

    float LineDistance(Vector3 point, Vector3 v1, Vector3 v2)
    {
        Vector3 lineDirection = v2 - v1;
        float lineLength = lineDirection.magnitude;
        if (lineLength == 0) return Vector3.Distance(point, v1); // 선이 점일 경우

        float t = Mathf.Clamp(Vector3.Dot(point - v1, lineDirection) / (lineLength * lineLength), 0, 1);
        Vector3 closestPoint = v1 + t * lineDirection;

        return Vector3.Distance(point, closestPoint); // 점과 선의 가장 가까운 지점 사이의 거리
    }

    void DrawDalgona()
    {
        // 원 모양
        if (dalgonaType == 0)
        {
            dalgonaRenderer.positionCount = segments + 1;
            float angle = 0f;

            for (int i = 0; i < segments + 1; i++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
                dalgonaRenderer.SetPosition(i, new Vector3(x, y, 0) + dalgonaCenter.localPosition);
                angle += (360f / segments);
            }
        }
        // 삼각형 모양
        else if (dalgonaType == 1)
        {
            dalgonaRenderer.positionCount = 4;
            float angle = 0f;

            for (int i = 0; i < 3; i++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
                dalgonaRenderer.SetPosition(i, new Vector3(x, y, 0) + dalgonaCenter.localPosition);
                angle += 120f; // 360도를 3등분
            }
            dalgonaRenderer.SetPosition(3, dalgonaRenderer.GetPosition(0));
        }
        // 사각형 모양
        else if (dalgonaType == 2)
        {
            dalgonaRenderer.positionCount = 5;
            float xOffset = radius;
            float yOffset = radius;

            dalgonaRenderer.SetPosition(0, new Vector3(-xOffset, -yOffset, 0) + dalgonaCenter.localPosition); 
            dalgonaRenderer.SetPosition(1, new Vector3(xOffset, -yOffset, 0) + dalgonaCenter.localPosition); 
            dalgonaRenderer.SetPosition(2, new Vector3(xOffset, yOffset, 0) + dalgonaCenter.localPosition); 
            dalgonaRenderer.SetPosition(3, new Vector3(-xOffset, yOffset, 0) + dalgonaCenter.localPosition);
            dalgonaRenderer.SetPosition(4, dalgonaRenderer.GetPosition(0));
        }
    }

    void SetPredefinedPoints()
    {
        predefinedPoints.Clear();

        // 원의 지점 설정
        if (dalgonaType == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                float angle = i * (360f / 4); // 90도 간격
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
                predefinedPoints.Add(new Vector3(x, y, 0) + dalgonaCenter.localPosition);
            }
        }
        // 삼각형의 지점 설정
        else if (dalgonaType == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                float angle = i * (360f / 3); // 120도 간격
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
                predefinedPoints.Add(new Vector3(x, y, 0) + dalgonaCenter.localPosition);
            }
        }
        // 사각형의 지점 설정
        else if (dalgonaType == 2)
        {
            predefinedPoints.Add(new Vector3(-radius, -radius, 0) + dalgonaCenter.localPosition); // 왼쪽 아래
            predefinedPoints.Add(new Vector3(radius, -radius, 0) + dalgonaCenter.localPosition);  // 오른쪽 아래
            predefinedPoints.Add(new Vector3(radius, radius, 0) + dalgonaCenter.localPosition);   // 오른쪽 위
            predefinedPoints.Add(new Vector3(-radius, radius, 0) + dalgonaCenter.localPosition);  // 왼쪽 위
        }
    }

    void CheckSuccess()
    {
        // 그리지 않았을 경우 반환
        if (targetPositionCount == 0) return;

        // 현재 지점을 지나고 있는지 확인
        if (currentTargetIndex < predefinedPoints.Count)
        {
            float distanceToTarget = Vector3.Distance(needleRenderer.GetPosition(targetPositionCount - 1), predefinedPoints[currentTargetIndex]);
            if (distanceToTarget < 0.3f)
            {
                currentTargetIndex++;
            }
        }

        // 모든 지점을 지나고 처음 지점으로 돌아왔는지 확인
        if (currentTargetIndex == predefinedPoints.Count)
        {
            float distanceToStart = Vector3.Distance(needleRenderer.GetPosition(targetPositionCount - 1), predefinedPoints[0]);
            if (distanceToStart < 0.3f)
            {
                isClear = true;

                // 성공 UI
                currentTargetIndex = predefinedPoints.Count + 1;

                resultUI.gameObject.transform.parent.gameObject.SetActive(true);
                resultUI.sprite = resultImg[1];

                FadeManager fadeMgr = fadeOut.GetComponent<FadeManager>();
                fadeMgr.isGameClear = true;

                Invoke("FadeOut", 0.3f);
            }
        }
    }


    void GameOver()
    {
        if (!isDrawing || isClear) return;

        isDrawing = false; // 그리기 중지

        // 실패 UI
        resultUI.gameObject.transform.parent.gameObject.SetActive(false);
        resultUI.gameObject.transform.parent.gameObject.SetActive(true);
        resultUI.sprite = resultImg[0];
        Invoke("DisableResultUI", 2.3f);

        ResultManager.Instance.tryCount++;
        Reset();
    }

    void FadeOut()
    {
        fadeOut.SetActive(true);
    }

    void DisableResultUI()
    {
        resultUI.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
