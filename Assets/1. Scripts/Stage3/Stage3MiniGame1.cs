using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3MiniGame1 : MiniGameManager
{
    public int maxRoundNum;
    [SerializeField] private Stage3MiniGame1StateSO _miniGameState;

    void Start()
    {
        _miniGameState.ResetState();
    }

    void Update()
    {
        if (_miniGameState.round >= maxRoundNum)
        {
            OnSuccessMiniGame();
        }
    }
}
