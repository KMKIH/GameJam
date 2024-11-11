using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stage3MiniGame1Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Stage3MiniGame1StateSO _miniGameState;
    public float dragDistanceThreshold;
    private RectTransform _targetTransform;
    private RectTransform _rectTransform;
    private int _dragCount;
    private float _ratio;
    private bool _movedUp = false;
    private bool _movedDown = false;
    private Vector2 _startPosition;
    private Vector2 _initPosition;

    [Header("Sound")]
    [SerializeField] AudioClip eatSound;
    SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = FindObjectOfType<SoundManager>();
    }
    void Start()
    {
        _targetTransform = GameObject.FindGameObjectsWithTag("Mini Game Goal")[0].GetComponent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();
        _dragCount = 0;
        _ratio = 1f;

        _initPosition =_rectTransform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        _startPosition = eventData.position;
        _movedUp = false;
        _movedDown = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

        if (RectTransformUtility.RectangleContainsScreenPoint(_targetTransform, eventData.position))
        {
            float dragDistance = eventData.position.y - _startPosition.y;

            if (MathF.Abs(dragDistance) >= dragDistanceThreshold && !_movedUp && !_movedDown)
            {
                _movedUp = dragDistance > 0;
                _movedDown = dragDistance < 0;
                _startPosition = eventData.position;
            }
            else if (dragDistance <= -dragDistanceThreshold && _movedUp)
            {
                _movedDown = true;
                IncrementCounter();
                ResetMovementFlags();
                _startPosition = eventData.position;
            }
            else if (dragDistance >= dragDistanceThreshold && _movedDown)
            {
                _movedUp = true;
                IncrementCounter();
                ResetMovementFlags();
                _startPosition = eventData.position;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _movedUp = false;
        _movedDown = false;

        _rectTransform.localPosition = _initPosition;
    }

    private void IncrementCounter()
    {
        _dragCount++;
        if (_dragCount >= 10)
        {
            _dragCount = 0;
            _miniGameState.round += 1;
            _soundManager.PlayEffect1(eatSound);
        }
    }

    private void ResetMovementFlags()
    {
        _movedUp = false;
        _movedDown = false;
    }
}
