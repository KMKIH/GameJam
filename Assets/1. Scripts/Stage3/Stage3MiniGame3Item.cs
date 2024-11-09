using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stage3MiniGame3Item : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Stage3MiniGame3StateSO _miniGameState;
    [SerializeField] private VoidEventChannelSO m_selected;
    public int index;
    public Text textUI;
    private string _paragraph;

    private void Start() { SetParagraph(); }

    private void Update() { SetParagraph(); }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }


    private void SetParagraph()
    {
        List<string> paragraphs = _miniGameState.startParagraphs;
        if (_miniGameState.currentParagraph == EParagraph.Middle)
        {
            paragraphs = _miniGameState.middleParagraphs;
        }
        else if (_miniGameState.currentParagraph == EParagraph.End)
        {
            paragraphs = _miniGameState.endParagraphs;
        }

        _paragraph = paragraphs[index];
        textUI.text = _paragraph;
    }

    public void OnClick()
    {
        if (!_miniGameState.buttonActive) return;
        Debug.Log(string.Format("{0}, {1}", _miniGameState.currentParagraph, _paragraph));
        if (_miniGameState.currentParagraph == EParagraph.Start)
        {
            _miniGameState.selectedStartParagraph = _paragraph;
        }
        else if (_miniGameState.currentParagraph == EParagraph.Middle)
        {
            _miniGameState.selectedMiddleParagraph = _paragraph;
        }
        else
        {
            _miniGameState.selectedEndParagraph = _paragraph;
        }

        m_selected.RaiseEvent();
    }
}
