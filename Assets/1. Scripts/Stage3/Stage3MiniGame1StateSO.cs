using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Stage3/Mini Game 1 State", fileName = "Mini Game 1 State")]
public class Stage3MiniGame1StateSO : ScriptableObject
{
    public int round;

    public void ResetState()
    {
        round = 0;
    }
}