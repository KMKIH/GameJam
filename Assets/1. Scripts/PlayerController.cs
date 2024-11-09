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

    PlayerState _state = PlayerState.Idle;
    [SerializeField] private GameStateSO _gameState;
    Vector3 _destPos;

    private NavMeshAgent _agent;
    private Rigidbody2D _rigidbody;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
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
        if (hit)
        {
            GameObject clickedObject = hit.collider.gameObject;

            if (hit.collider != null)
            {
                _agent.SetDestination(hit.point);
                _gameState.targetObject = clickedObject;
            }
        }
    }
}