using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class Stage1MiniGame1Item : MonoBehaviour
{
    [SerializeField] private Stage1MiniGame1ItemSO _miniGameItemData;
    private RectTransform _targetUI;
    private Camera _mainCamera;
    private bool _isGrabbed;
    private RectTransform _rectTransform;
    private Rigidbody2D _rigidbody;

    void Start()
    {
        _mainCamera = Camera.main;
        _targetUI = GameObject.FindGameObjectsWithTag("Mini Game Goal")[0].GetComponent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = new Vector3(_miniGameItemData.posX, _miniGameItemData.posY);
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.simulated = false;
        _isGrabbed = false;
    }

    void Update()
    {
        if (IsOverlapping(_targetUI))
        {
            Debug.Log($"{gameObject.name}가 {_targetUI.name}와 겹쳤습니다!");
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
        if (Input.GetMouseButton(0) && _isGrabbed)
        {
            _rectTransform.position = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButtonUp(0) && _isGrabbed)
        {
            _isGrabbed = false;
            _rigidbody.simulated = true;
            _rigidbody.velocity = new Vector3(0, -100f, 0);
        }
    }
    
    private void OnClick()
    {
        GraphicRaycaster raycaster = GetComponentInParent<GraphicRaycaster>();
        
        if (raycaster != null)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();

            raycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject == gameObject)
                {
                    _isGrabbed = true;
                }
            }
        }
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
