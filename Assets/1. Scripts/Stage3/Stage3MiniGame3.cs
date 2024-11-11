using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stage3MiniGame3 : MiniGameManager
{
    [SerializeField] private Stage3MiniGame3StateSO _miniGameState;
    [SerializeField] private VoidEventChannelSO m_selected;

    [Header("Images")]
    [SerializeField] private GameObject _gameObject1;
    [SerializeField] private GameObject _gameObject2;

    [SerializeField] private Image _clearImage1;
    [SerializeField] private Image _clearImage2;

    void Start()
    {
        _miniGameState.ResetState();

        _clearImage1.DOFade(0, 0);
        _clearImage1.gameObject.SetActive(false);
        _clearImage2.DOFade(0, 0);
        _clearImage2.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        m_selected.OnEventRaised -= OnSelect;
        m_selected.OnEventRaised += OnSelect;
    }
    private void OnDisable()
    {
        m_selected.OnEventRaised -= OnSelect;
    }

    public void OnSelect()
    {
        UpdateParagraph();
    }

    async void UpdateParagraph()
    {
        _miniGameState.buttonActive = false;
        await UniTask.WaitForSeconds(2);

        _miniGameState.buttonActive = true;
        if (_miniGameState.currentParagraph == EParagraph.Start)
        {
            _miniGameState.currentParagraph = EParagraph.Middle;
        }
        else if (_miniGameState.currentParagraph == EParagraph.Middle)
        {
            _miniGameState.currentParagraph = EParagraph.End;
        }

        if (_miniGameState.selectedEndParagraph.Length > 0)
        {
            _gameObject1.SetActive(false);
            _clearImage1.gameObject.SetActive(true);
            _clearImage1.DOFade(1, 1);
            await UniTask.WaitForSeconds(2f);
            _gameObject2.SetActive(false);
            _clearImage2.gameObject.SetActive(true);
            _clearImage2.DOFade(1, 1);
            await UniTask.WaitForSeconds(2f);
            OnSuccessMiniGame();
        }
    }

}
