using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/GameState", fileName = "Game State")]
public class GameStateSO : ScriptableObject
{
    public GameObject targetObject;

    public void ResetGameState()
    {
        targetObject = null;
    }
}