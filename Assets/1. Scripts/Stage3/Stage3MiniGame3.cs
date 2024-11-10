using System.Collections;
using UnityEngine;

public class Stage3MiniGame3 : MiniGameManager
{
    [SerializeField] private Stage3MiniGame3StateSO _miniGameState;
    [SerializeField] private VoidEventChannelSO m_selected;

    void Start()
    {
        _miniGameState.ResetState();
    }


    private void OnEnable()
    {
        m_selected.OnEventRaised += OnSelect;
    }
    private void OnDisable()
    {
        m_selected.OnEventRaised -= OnSelect;
    }

    public void OnSelect()
    {
        StartCoroutine(UpdateParagraph());
    }

    IEnumerator UpdateParagraph()
    {
        _miniGameState.buttonActive = false;
        yield return new WaitForSeconds(2f);
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
            OnSuccessMiniGame();
        }
    }

}
