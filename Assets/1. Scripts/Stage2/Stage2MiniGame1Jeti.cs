using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2MiniGame1Jeti : MonoBehaviour
{
    public Canvas canvas; // UI가 위치한 캔버스
    void Update()
    {
        float halfScreenWidth = Screen.width / 2;

        // 마우스가 화면 왼쪽에 있을 때
        if (Input.mousePosition.x > halfScreenWidth)
        {
            // 마우스 위치를 UI 로컬 좌표로 변환
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out localPoint);

            // UI 오브젝트의 위치를 마우스 위치로 업데이트
            transform.localPosition = localPoint;
        }
    }
}
