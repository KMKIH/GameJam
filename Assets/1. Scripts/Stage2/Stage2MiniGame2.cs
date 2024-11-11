using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2MiniGame2 : MiniGameManager
{
    [SerializeField] private Stage2MiniGame2SO _miniGameState;
    private bool _isClear = false;

    void Start()
    {
        _miniGameState.ResetState();
    }

    void Update()
    {
        if (_miniGameState.solvedPuzzles == 3 && _isClear == false)
        {
            _isClear = true;
            OnSuccessMiniGame();
        }
    }
}
