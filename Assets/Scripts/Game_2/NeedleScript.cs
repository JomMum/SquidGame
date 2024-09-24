using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleScript : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // 스크린 좌표를 월드 좌표로 변환
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        // Z 좌표는 현재 오브젝트의 Z 값을 유지
        mousePosition.z = transform.position.z;

        // needle 오브젝트를 마우스 위치로 이동
        transform.position = mousePosition;
    }
}
