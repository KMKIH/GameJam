using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2MiniGame1Jeti : MonoBehaviour
{
    public Canvas canvas; // UI�� ��ġ�� ĵ����
    void Update()
    {
        float halfScreenWidth = Screen.width / 2;

        // ���콺�� ȭ�� ���ʿ� ���� ��
        if (Input.mousePosition.x > halfScreenWidth)
        {
            // ���콺 ��ġ�� UI ���� ��ǥ�� ��ȯ
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out localPoint);

            // UI ������Ʈ�� ��ġ�� ���콺 ��ġ�� ������Ʈ
            transform.localPosition = localPoint;
        }
    }
}
