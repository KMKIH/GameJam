using System.Collections.Generic;
using UnityEngine;

public enum EParagraph
{
    Start,
    Middle,
    End
};

[CreateAssetMenu(menuName = "State/Stage3/Mini Game 3 State", fileName = "Mini Game 3 State")]
public class Stage3MiniGame3StateSO : ScriptableObject
{
    public bool buttonActive;
    public EParagraph currentParagraph;

    public string? selectedStartParagraph;
    public string? selectedMiddleParagraph;
    public string? selectedEndParagraph;

    public List<string> startParagraphs;
    public List<string> middleParagraphs;
    public List<string> endParagraphs;

    public void ResetState()
    {
        buttonActive = true;
        currentParagraph = EParagraph.Start;
        selectedStartParagraph = null;
        selectedMiddleParagraph = null;
        selectedEndParagraph = null;
    }
}
