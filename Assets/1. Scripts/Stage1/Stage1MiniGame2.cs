using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1MiniGame2 : MiniGameManager
{
    Stage1MiniGame2 gameManager;
    [SerializeField] private Stage1MiniGame2StateSO _miniGame2State;

    void Start()
    {
        gameManager = FindObjectOfType<Stage1MiniGame2>();
        _miniGame2State.ResetState();
    }

    IEnumerator EndCurrentGame()
    {
        _miniGame2State.isControllerAvailable = false;
        yield return new WaitForSeconds(2f);
        OnSuccessMiniGame();
    }
    IEnumerator ResetChannel()
    {
        _miniGame2State.isControllerAvailable = false;
        _miniGame2State.channel = "";
        yield return new WaitForSeconds(2f);
        _miniGame2State.isControllerAvailable = true;
    }
    private void OnAddNumber(int val)
    {
        if (gameManager._gameState.MiniGameState != MiniGameState.OnGoing) return;
        if (!_miniGame2State.isControllerAvailable) return;
        if (_miniGame2State.channel.Length < 3)
        {
            _miniGame2State.channel += val.ToString();
        }
        if (_miniGame2State.channel.Length == 3)
        {
            _miniGame2State.isChannelChanged = true;
        }
        if (_miniGame2State.channel.Length == 3 && 
            _miniGame2State.channel == "175")
        {
            StartCoroutine(EndCurrentGame());
        }
        else if (_miniGame2State.channel.Length == 3)
        {
            StartCoroutine(ResetChannel());
        }
    }


    public void OnClick0() { OnAddNumber(0); }
    public void OnClick1() { OnAddNumber(1); }
    public void OnClick2() { OnAddNumber(2); }
    public void OnClick3() { OnAddNumber(3); }
    public void OnClick4() { OnAddNumber(4); }
    public void OnClick5() { OnAddNumber(5); }
    public void OnClick6() { OnAddNumber(6); }
    public void OnClick7() { OnAddNumber(7); }
    public void OnClick8() { OnAddNumber(8); }
    public void OnClick9() { OnAddNumber(9); }
}
