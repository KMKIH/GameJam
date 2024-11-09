using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameStateSO _gameState;

    private NavMeshAgent _agent;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = 10;
        _agent.acceleration = 20;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToClickPosition();
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
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        // 이동
        if (hit && _gameState.playerState == PlayerState.FocusLeft)
        {
            GameObject clickedObject = hit.collider.gameObject;

            if (hit.collider != null)
            {
                _agent.SetDestination(hit.point);
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