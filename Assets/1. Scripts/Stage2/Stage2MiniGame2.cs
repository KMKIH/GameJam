using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2MiniGame2 : MiniGameManager
{
    [SerializeField] private Stage2MiniGame2SO _miniGameState;

    void Start()
    {
        _miniGameState.ResetState();
    }

    void Update()
    {
        if (_miniGameState.solvedPuzzles == 6)
        {
            OnSuccessMiniGame();
        }
    }

    public override void StartMiniGame()
    {
        Instantiate(this.gameObject);
    }
    public override void EndMiniGame()
    {
        Destroy(this.gameObject);
    }
}
