using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Stage2/Mini Game 2 State", fileName = "Stage 2 Mini Game 2 State")]
public class Stage2MiniGame2SO : ScriptableObject
{
    public int solvedPuzzles;

    public void ResetState()
    {
        solvedPuzzles = 0;
    }
}
