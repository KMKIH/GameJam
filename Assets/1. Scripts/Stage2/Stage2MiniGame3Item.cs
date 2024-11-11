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

    private RectTransform _itemRectTransform; // 지우개
    [SerializeField] private RectTransform _backgroundTransform; // 배경
    private Vector2 _initPosition;


    private int _dragCount = 0;
    private int _maxDragCount = 20;

    private Vector2 _lastDragDirection; // 마지막 드래그 방향
    private Vector2 _lastPosition; // 이전 위치
    private bool _isFirstDrag = true; // 첫 드래그 여부
    private bool _isClear = false;

    [Header("Sound")]
    [SerializeField] AudioClip _removeSound;
    SoundManager _soundManager;


    void Start()
    {
        _soundManager = FindObjectOfType<SoundManager>();

        GameObject targetObject = GameObject.FindGameObjectsWithTag("Mini Game Goal")[0];
        _targetTransform = targetObject.GetComponent<RectTransform>();
        _targetImage = targetObject.GetComponent<Image>();

        _itemRectTransform = GetComponent<RectTransform>();
        _initPosition = _itemRectTransform.localPosition;
    }

    void Update()
    {
        _targetImage.color = new Color(
            _targetImage.color.r,
            _targetImage.color.g,
            _targetImage.color.b,
            1 - (float)_dragCount/_maxDragCount);

        if (Input.GetMouseButtonUp(0))
        {
            // 드래그가 끝났을 때 targetArea 안에 있는지 확인
            if (!IsRectOverlapping(_backgroundTransform, _itemRectTransform))
            {
                _itemRectTransform.localPosition = _initPosition;
            }
        }
    }
    private void OnDestroy()
    {
        _soundManager.StopEffect1();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        _lastPosition = eventData.position;

        _soundManager.PlayEffect1(_removeSound, 1, true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

        if (RectTransformUtility.RectangleContainsScreenPoint(_targetTransform, eventData.position))
        {

            Vector2 currentPosition = eventData.position;
            Vector2 dragDirection = (currentPosition - _lastPosition).normalized;
            float dragDistance = Vector2.Distance(currentPosition, _lastPosition);

            // 드래그 거리가 임계값을 넘었을 때만 처리
            if (dragDistance >= dragDistanceThreshold)
            {
                if (_isFirstDrag)
                {
                    // 첫 드래그일 경우, 방향만 저장
                    _lastDragDirection = dragDirection;
                    _isFirstDrag = false;
                }
                else
                {
                    // 현재 방향과 이전 방향의 각도 계산
                    float angle = Vector2.Angle(_lastDragDirection, dragDirection);

                    // 각도가 150도 이상이면 반대 방향으로 간주
                    // (정확히 180도가 아닌 150도로 설정하여 약간의 여유를 둠)
                    if (angle >= 150f)
                    {
                        IncrementCounter();
                        _isFirstDrag = true;
                    }

                    _lastDragDirection = dragDirection;
                }
                _lastPosition = currentPosition;
            }
        }
    }
    private bool IsRectOverlapping(RectTransform area, RectTransform element)
    {
        // UI 요소가 지정된 영역 안에 있는지 확인
        Vector3[] corners = new Vector3[4];
        element.GetWorldCorners(corners);

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, corners[0]);
        return RectTransformUtility.RectangleContainsScreenPoint(area, screenPoint) &&
               RectTransformUtility.RectangleContainsScreenPoint(area,
                   RectTransformUtility.WorldToScreenPoint(null, corners[2]));
    }
    private void IncrementCounter()
    {
        _dragCount++;
        if (_dragCount >= _maxDragCount && _isClear == false)
        {
            _isClear = true;
            m_gameClear.RaiseEvent();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _soundManager.StopEffect1();
    }
}
