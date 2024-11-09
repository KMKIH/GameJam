using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGameManager : MonoBehaviour
{
    [SerializeField] GameStateSO _gameState;
    public abstract void StartMiniGame();
    public abstract void EndMiniGame();
    public void OnSuccessMiniGame()
    {
        _gameState.MiniGameState = MiniGameState.Success;
    }
    public void OnFailedMiniGame()
    {
        _gameState.MiniGameState = MiniGameState.Failed;
    }
}
