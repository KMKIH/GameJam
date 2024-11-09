using UnityEngine;
using UnityEngine.EventSystems;

public class Stage3MiniGame2Hand : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private VoidEventChannelSO m_collided;
    private RectTransform _targetUI;
    private RectTransform _rectTransform;
    private bool _isMovable = true;

    void Start()
    {
        _targetUI = GameObject
                .FindGameObjectsWithTag("Mini Game Target")[0]
                .GetComponent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();

    }

    void Update()
    {
        if (IsOverlapping(_targetUI))
        {
            _isMovable = false;
            _targetUI.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            _targetUI.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
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
        return rect1.Overlaps(rect2);
    }

    Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }
}
