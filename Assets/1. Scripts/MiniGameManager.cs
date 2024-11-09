using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGameManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] public GameStateSO _gameState;
    [Header("GID")]
    [SerializeField] public int gid;
    public abstract void StartMiniGame();
    public abstract void EndMiniGame();
    public void OnSuccessMiniGame()
    {
        _gameState.MiniGameState = MiniGameState.Success;
        EndMiniGame();
    }
}
