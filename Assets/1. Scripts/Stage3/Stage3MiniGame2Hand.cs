using UnityEngine;
using UnityEngine.EventSystems;

public class Stage3MiniGame2Hand : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private VoidEventChannelSO m_collided;
    public RectTransform targetRectTransform;
    private RectTransform _rectTransform;
    private bool _isMovable = true;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (m_collided == null)
        {
            Debug.LogError("m_collided is not assigned!");
            return;
        }
        if (IsOverlapping(targetRectTransform) && _isMovable)
        {
            _isMovable = false;
            m_collided.RaiseEvent();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_isMovable) return;
        transform.position = new Vector2(Input.mousePosition.x, transform.position.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isMovable) return;
        transform.position = new Vector2(Input.mousePosition.x, transform.position.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isMovable) return;
        transform.position = new Vector2(Input.mousePosition.x, transform.position.y);
    }


    bool IsOverlapping(RectTransform other)
    {
        Rect rect1 = RectTransformToScreenSpace(_rectTransform);
        Rect rect2 = RectTransformToScreenSpace(other);

       return rect1.xMin <= rect2.xMax && rect1.xMax >= rect2.xMin &&
                          rect1.yMin <= rect2.yMax && rect1.yMax >= rect2.yMin;
    }

    Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 screenPointMin = RectTransformUtility.WorldToScreenPoint(null, transform.TransformPoint(transform.rect.min));
        Vector2 screenPointMax = RectTransformUtility.WorldToScreenPoint(null, transform.TransformPoint(transform.rect.max));
        Vector2 size = screenPointMax - screenPointMin;

        return new Rect(screenPointMin, size);
    }
}
