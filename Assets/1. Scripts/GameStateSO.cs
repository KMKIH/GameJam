using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    FocusLeft,
    CutScene,
    MiniGame,
}
[CreateAssetMenu(menuName = "State/GameState", fileName = "Game State")]
public class GameStateSO : ScriptableObject
{
    public GameObject targetObject;
    public PlayerState playerState;

    public void ResetGameState()
    {
        targetObject = null;
    }
}