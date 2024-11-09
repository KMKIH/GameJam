using UnityEngine;
using UnityEngine.UI;

public class Stage3MiniGame3Letter : MonoBehaviour
{
    [SerializeField] private Stage3MiniGame3StateSO _miniGameState;
    [SerializeField] private VoidEventChannelSO m_selected;

    public Text paragraphUI;

    private void OnEnable()
    {
        m_selected.OnEventRaised += UpdateUI;
    }
    private void OnDisable()
    {
        m_selected.OnEventRaised -= UpdateUI;   
    }

    public void UpdateUI()
    {
        paragraphUI.text = string.Format(
            "{0}\n\n{1}\n\n{2}",
            _miniGameState.selectedStartParagraph,
            _miniGameState.selectedMiddleParagraph,
            _miniGameState.selectedEndParagraph);
    }
}
