using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    enum PlayerState
    {
        Idle,
        Moving,
    }

    [SerializeField]
    float _speed = 10;

    PlayerState _state = PlayerState.Idle;
    Vector3 _destPos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClicked();
        }
        switch (_state)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
        }
    }

    void UpdateIdle()
    {

    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;

        if (dir.magnitude < 0.01f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);
        }
    }

    void OnMouseClicked()
    {
        _state = PlayerState.Moving;
        _destPos = (Vector2) (Camera.main.ScreenToWorldPoint(Input.mousePosition));

        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
        if (hitInformation.collider != null)
        {
            Debug.Log(hitInformation.collider.gameObject.name);
        }
    }
}