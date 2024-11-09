using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    FocusLeft,
    CutScene,
    MiniGame,
}
public enum MiniGameState
{
    OnGoing,
    Success,
    Failed,
    Pause,
}
[CreateAssetMenu(menuName = "State/GameState", fileName = "Game State")]
public class GameStateSO : ScriptableObject
{
    public GameObject targetObject;
    public MiniGameManager targetMiniGame;

    public PlayerState playerState;
    private MiniGameState _miniGameState;
    public MiniGameState MiniGameState
    {
        get { return _miniGameState; }
        set
        {
            if (_miniGameState != value)
            {
                _miniGameState = value;
                OnMiniGameStateChanged(_miniGameState);
            }
        }
    }
    public event Action<MiniGameState> OnMiniGameStateChanged;

    public void ResetGameState()
    {
        targetObject = null;
        targetMiniGame = null;
    }
    public void StartMiniGame()
    {
        MiniGameState = MiniGameState.OnGoing;
        targetMiniGame.StartMiniGame();
    }
}