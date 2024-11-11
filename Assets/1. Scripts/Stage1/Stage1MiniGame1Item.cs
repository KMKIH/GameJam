using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Stage1MiniGame1Item : MonoBehaviour
{
    [SerializeField] Stage1MiniGame1 gameManager;

    public bool isRightItem;

    private bool _inOnTheBag = false;

    private RectTransform _targetUI;
    private bool _isGrabbed;
    private RectTransform _rectTransform;

    [Header("Sound")]
    [SerializeField] AudioClip _grapSound;
    [SerializeField] AudioClip _dropSound;
    SoundManager _soundManager;
    [SerializeField] float effectVolume = 0.5f;

    void Awake()
    {
        _targetUI = GameObject.FindGameObjectsWithTag("Mini Game Goal")[0].GetComponent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();
        _soundManager = FindObjectOfType<SoundManager>();
        _isGrabbed = false;
    }

    void Update()
    {
        if (gameManager._gameState.MiniGameState != MiniGameState.OnGoing) return;

        // Grab
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
            if (_isGrabbed) _soundManager.PlayEffect1(_grapSound, effectVolume);
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
            _soundManager.PlayEffect1(_dropSound, effectVolume);
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
