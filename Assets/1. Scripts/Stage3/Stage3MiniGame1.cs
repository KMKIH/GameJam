using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Stage3MiniGame1 : MiniGameManager
{
    public int maxRoundNum;
    [SerializeField] private Stage3MiniGame1StateSO _miniGameState;

    [Header("Objects")]
    [SerializeField] Image trace;
    [SerializeField] Transform candy;

    private bool _isClear = false;

    void Start()
    {
        _miniGameState.ResetState();

        trace.DOFade(0, 0);
        candy.DOScale(1, 0);
    }

    void Update()
    {
        if (_miniGameState.round >= maxRoundNum && _isClear == false)
        {
            _isClear = true;

            trace.DOFade(1f, 0);
            candy.DOScale(0, 0);
            OnSuccessMiniGame();
        }
        else if (_miniGameState.round >= maxRoundNum / 4 * 3)
        {
            trace.DOFade(0.75f, 0);
            candy.DOScale(0.25f, 0);
        }
        else if (_miniGameState.round >= maxRoundNum / 2)
        {
            trace.DOFade(0.5f, 0);
            candy.DOScale(0.5f, 0);
        }
        else if (_miniGameState.round >= maxRoundNum / 4)
        {
            trace.DOFade(0.25f, 0);
            candy.DOScale(0.75f, 0);
        }
    }
}
