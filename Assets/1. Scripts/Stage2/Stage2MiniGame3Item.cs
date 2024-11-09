using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stage2MiniGame3Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private VoidEventChannelSO m_gameClear;
    public float size;
    public float dragDistanceThreshold;
    private RectTransform _targetTransform;
    private Image _targetImage;
    private int _dragCount;
    private bool _movedUp = false;
    private bool _movedDown = false;
    private Vector2 _startPosition;

    void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectsWithTag("Mini Game Goal")[0];
        _targetTransform = targetObject.GetComponent<RectTransform>();
        _targetImage = targetObject.GetComponent<Image>();
    }

    void Update()
    {
        _targetImage.color = new Color(
            _targetImage.color.r,
            _targetImage.color.g,
            _targetImage.color.b,
            1 - 0.02f * _dragCount);
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
    }

    private void IncrementCounter()
    {
        _dragCount++;
        if (_dragCount >= 50)
        {
            m_gameClear.RaiseEvent();
        }
    }

    private void ResetMovementFlags()
    {
        _movedUp = false;
        _movedDown = false;
    }
}
