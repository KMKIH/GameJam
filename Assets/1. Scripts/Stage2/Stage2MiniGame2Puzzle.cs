using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stage2MiniGame2Puzzle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Stage2MiniGame2SO _miniGameState;
    public float targetX;
    public float targetY;
    private RectTransform _rectTransform;
    private Vector3 _startPosition;
    private bool _isDraggable;

    void Start()
    {
        _isDraggable = true;
        _rectTransform = GetComponent<RectTransform>();
        _startPosition = _rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_isDraggable) return;
        transform.position = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDraggable) return;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isDraggable) return;
        if (CheckIsCorrectPosition())
        {
            StartCoroutine(GoToTargetPosition());        
        }
        else
        {
            StartCoroutine(SnapBackToStartPosition());
        }
    }

    private IEnumerator GoToPosition(Vector3 pos)
    {
        float duration = 0.3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            _rectTransform.anchoredPosition = Vector3.Lerp(_rectTransform.anchoredPosition, pos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _rectTransform.anchoredPosition = pos;
    }

    private IEnumerator GoToTargetPosition()
    {
        _isDraggable = false;
        _miniGameState.solvedPuzzles += 1;
        yield return GoToPosition(new Vector3(targetX, targetY));
    }
    private IEnumerator SnapBackToStartPosition()
    {
        yield return GoToPosition(_startPosition);
    }

    private bool CheckIsCorrectPosition()
    {
        return Math.Abs(_rectTransform.anchoredPosition.x - targetX) <= _rectTransform.sizeDelta.x / 2 &&
            Math.Abs(_rectTransform.anchoredPosition.y - targetY) <= _rectTransform.sizeDelta.y / 2;   
    }
}
