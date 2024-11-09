using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGameManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] public GameStateSO _gameState;
    [Header("GID")]
    [SerializeField] public int gid;


    static GameObject realObject;
    public void StartMiniGame()
    {
        realObject = Instantiate(this.gameObject);
    }
    public void EndMiniGame()
    {
        if (realObject != null && realObject.activeSelf)
            Destroy(realObject);
    }
    public void OnSuccessMiniGame()
    {
        _gameState.MiniGameState = MiniGameState.Success;
        EndMiniGame();
    }
}
