using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameStateSO _gameState;

    private NavMeshAgent _agent;
    private Camera _mainCamera;
    private Animator _anim;
    private SpriteRenderer _sr;

    enum LastLookDir
    {
        Front, Back, Left, Right
    }
    LastLookDir lookDir = LastLookDir.Front;

    private void Start()
    {
        _mainCamera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = 10;
        _agent.acceleration = 20;

        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToClickPosition();
        }



        // 애니메이션
        if (_agent.hasPath)
        {
            var xDir = _agent.velocity.normalized.x;
            var yDir = _agent.velocity.normalized.y;
            if (Mathf.Abs(xDir) > Mathf.Abs(yDir))
            {
                _anim.Play("MoveSide");
                if (xDir > 0)
                {
                    _sr.flipX = true;
                    lookDir = LastLookDir.Right;
                }
                else
                {
                    _sr.flipX = false;
                    lookDir = LastLookDir.Left;
                }
            }
            else if (yDir > 0)
            {
                _anim.Play("MoveBack");
                lookDir = LastLookDir.Back;
            }
            else
            {
                _anim.Play("MoveFront");
                lookDir = LastLookDir.Front;
            }
        }
        else
        {
            switch (lookDir)
            {
                case LastLookDir.Front:
                    _anim.Play("IdleFront");
                    break;
                case LastLookDir.Back:
                    _anim.Play("IdleBack");
                    break;
                case LastLookDir.Left:
                    _sr.flipX = false;
                    _anim.Play("IdleSide");
                    break;
                case LastLookDir.Right:
                    _sr.flipX = true;
                    _anim.Play("IdleSide");
                    break;
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (_gameState.targetObject == other.gameObject)
        {
            _agent.ResetPath();
            _agent.velocity = Vector3.zero;
        }
    }

    private void MoveToClickPosition()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

        foreach (var hit in hits)
        {
            // 이동
            if (hit && _gameState.playerState == PlayerState.FocusLeft)
            {
                if (hit.collider != null)
                {
                    // 이동
                    _agent.SetDestination(hit.point);
                }

                // clicked Object 저장
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.TryGetComponent(out NavMeshModifier navMeshModifier) && navMeshModifier.area == 1)
                {
                    _gameState.targetObject = clickedObject;
                }
            }

            // 이동 요청
            else if (hit)
            {
                Debug.Log("Request Focus Left");
            }
        }
    }
}