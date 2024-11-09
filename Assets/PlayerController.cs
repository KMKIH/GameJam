using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private NavMeshAgent _agent;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToClickPosition();
        }
    }

    void MoveToClickPosition()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null)
        {
            _agent.SetDestination(hit.point);
        }
    }
}