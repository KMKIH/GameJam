using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    // CameraConfiner
    [SerializeField]
    BoxCollider2D cameraConfiner;

    // Boundary
    Vector2 minPos;
    Vector2 maxPos;

    private void Start()
    {
        minPos = (Vector2)cameraConfiner.transform.position - cameraConfiner.size * 0.4479167f;
        maxPos = (Vector2)cameraConfiner.transform.position + cameraConfiner.size * 0.4479167f;
    }
    void Update()
    {
        // 현재 위치에서 X, Y 위치를 제한
        float clampedX = Mathf.Clamp(transform.position.x, minPos.x, maxPos.x);
        float clampedY = Mathf.Clamp(transform.position.y, minPos.y, maxPos.y);

        // 제한된 위치로 플레이어 위치 고정
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
