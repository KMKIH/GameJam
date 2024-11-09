using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class Stage1MiniGame1Item : MonoBehaviour
{
    [SerializeField] Stage1MiniGame1 gameManager;
    [SerializeField] private Stage1MiniGame1ItemSO _miniGameItemData;

    public bool isRightItem;

    private RectTransform _targetUI;
    private bool _isGrabbed;
    private RectTransform _rectTransform;

    void Awake()
    {
        _targetUI = GameObject.FindGameObjectsWithTag("Mini Game Goal")[0].GetComponent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();
        _isGrabbed = false;
    }

    void Update()
    {
        if (gameManager._gameState.MiniGameState != MiniGameState.OnGoing) return;

        // 오브젝트 잃었을때 대비
        if(_targetUI == null) _targetUI = GameObject.FindGameObjectsWithTag("Mini Game Goal")[0].GetComponent<RectTransform>();
        if(_rectTransform) _rectTransform = GetComponent<RectTransform>();

        // Grab
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
        // Drag
        if (Input.GetMouseButton(0) && _isGrabbed)
        {
            _rectTransform.position = Input.mousePosition;
            return;
        }
        // Drop
        if (Input.GetMouseButtonUp(0) && _isGrabbed)
        {
            _isGrabbed = false;
            if(IsOverlapping(_targetUI))
            {
                if (isRightItem)
                {
                    gameManager.CountSuccess++;
                }
                gameManager.Count++;
                // Debug.Log($"{gameObject.name}가 {_targetUI.name}와 겹쳤습니다!");
                Destroy(gameObject);
            }
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
