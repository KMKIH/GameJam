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

    private bool _isWalking;

    [SerializeField] RectTransform _renderTexture;

    [Header("Sound")]
    [SerializeField] AudioClip[] walkSounds;
    [SerializeField] float _soundInterval = 0.5f;
    float _lastSoundTime = 0f;
    SoundManager _soundManager;

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

        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _soundManager = FindObjectOfType<SoundManager>();   
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
            _isWalking = true;

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
            _isWalking = false;

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

        // Sound
        if (_isWalking)
        {
            var curTime = Time.time;
            if(curTime > _lastSoundTime + _soundInterval)
            {
                _lastSoundTime = curTime;
                _soundManager.PlayEffect1(walkSounds[Random.Range(0, walkSounds.Length)],0.4f);
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
        Vector2 localPoint;
        // 마우스 위치를 렌더 텍스처 RectTransform 기준의 로컬 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _renderTexture,
            Input.mousePosition,
            null,
            out localPoint
        );

        // 렌더 텍스처의 크기 비율에 맞게 로컬 좌표를 정규화
        Vector2 normalizedPoint = new Vector2(
            (localPoint.x / _renderTexture.rect.width) + 0.5f,
            (localPoint.y / _renderTexture.rect.height) + 0.5f
        );

        // 카메라가 바라보는 월드 좌표를 계산
        Ray ray = _mainCamera.ViewportPointToRay(normalizedPoint);
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
                if (clickedObject.TryGetComponent(out NavMeshModifier navMeshModifier) && navMeshModifier.area == 3)
                {
                    Debug.Log(clickedObject);
                    _gameState.targetObject = clickedObject.gameObject;
                    break;
                }
                else
                {
                    _gameState.targetObject = null;
                }
            }

            // 이동 요청
            else if (hit && _gameState.playerState == PlayerState.MiniGame && hit.collider.gameObject.layer != LayerMask.NameToLayer("UI"))
            {
                FindObjectOfType<GameManager>().TurnExitPopUp(true);
            }
        }
    }
}