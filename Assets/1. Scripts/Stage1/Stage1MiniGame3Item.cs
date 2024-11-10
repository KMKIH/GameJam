using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1MiniGame3Item : MonoBehaviour
{
    Stage1MiniGame3 gameManager;
    [SerializeField] private Stage1MiniGame3StateSO _miniGame3State;
    [SerializeField] private VoidEventChannelSO m_foodEat;
    private RectTransform _targetUI;
    public Stage1MiniGame3ItemSO miniGameItemData;
    private RectTransform _rectTransform;
    private Vector2 _originalPos;

    void Start()
    {
        gameManager = FindObjectOfType<Stage1MiniGame3>();
        _targetUI = GameObject.FindGameObjectsWithTag("Mini Game Goal")[0].GetComponent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();
        _originalPos = _rectTransform.anchoredPosition;
    }
    void Update()
    {
        if (gameManager._gameState.MiniGameState != MiniGameState.OnGoing) return;

        // 먹음
        if (RectTransformToScreenSpace(_rectTransform).xMin <= -950f)
        {
            if (miniGameItemData.isRightItem)
            {
                _miniGame3State.life += 1;
            }
            else if (_miniGame3State.life > 0)
            {
                _miniGame3State.life -= 1;
            }
            FindObjectOfType<Stage1MiniGame3Goal>().PlayEat();
            EatFood();
            return;
        }

        // 입이 닫혀 있다면 Back
        if (!_miniGame3State.isReverse && !_miniGame3State.isMouseOpen && RectTransformToScreenSpace(_rectTransform).xMin <= -770f)
        {
            _miniGame3State.isReverse = true;
        }

        // 뒤로 이동
        if (_miniGame3State.isReverse)
        {
            _rectTransform.anchoredPosition = new Vector2(
                _rectTransform.anchoredPosition.x + Time.deltaTime * 400f, 
                _rectTransform.anchoredPosition.y);
            if(RectTransformToScreenSpace(_rectTransform).xMin >= 55f)
            {
                EatFood();
            }
            return;
        }
        // 앞으로 이동
        else
        { 
            _rectTransform.anchoredPosition = new Vector2(
                _rectTransform.anchoredPosition.x - Time.deltaTime * 400f,
                _rectTransform.anchoredPosition.y);
        }
        
        
    }

    bool IsOverlapping(RectTransform other)
    {
        Rect rect1 = RectTransformToScreenSpace(_rectTransform);
        Rect rect2 = RectTransformToScreenSpace(other);
        return rect1.Overlaps(rect2);
    }
    bool IsContained(RectTransform other)
    {
        Rect rect1 = RectTransformToScreenSpace(_rectTransform);
        Rect rect2 = RectTransformToScreenSpace(other);
        return rect2.Contains(rect1.min) && rect2.Contains(rect1.max);
    }

    Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.anchoredPosition - (size * 0.5f), size);
    }

    void EatFood()
    {
        FindObjectOfType<Stage1MiniGame3Goal>().PlayAH();
        _miniGame3State.isMouseOpen = true;
        _miniGame3State.index = Random.Range(0, _miniGame3State.foods.Count);
        m_foodEat.RaiseEvent();
        Destroy(gameObject);
    }
}
