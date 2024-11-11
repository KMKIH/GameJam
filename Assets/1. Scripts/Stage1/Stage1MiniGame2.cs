using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stage1MiniGame2 : MiniGameManager
{
    Stage1MiniGame2 gameManager;
    [SerializeField] private Stage1MiniGame2StateSO _miniGame2State;

    [SerializeField] Text _countText;

    [Header("Sound")]
    [SerializeField] AudioClip _clickSound;
    SoundManager _soundManager;
    void Start()
    {
        gameManager = FindObjectOfType<Stage1MiniGame2>();
        _soundManager = FindObjectOfType<SoundManager>();
        _miniGame2State.ResetState();
        _countText.text = "      ";
    }
    void Update()
    {
        var first = _miniGame2State.channel.Length >= 1 ? _miniGame2State.channel[0] : ' ';
        var second = _miniGame2State.channel.Length >= 2 ? _miniGame2State.channel[1] : ' ';
        var third = _miniGame2State.channel.Length >= 3 ? _miniGame2State.channel[2] : ' ';
        _countText.text = $"{first} {second} {third}";
    }
    private void OnDestroy()
    {
        _soundManager.StopEffect1();
        _soundManager.StopEffect2();
    }

    IEnumerator EndCurrentGame()
    {
        _miniGame2State.isControllerAvailable = false;
        yield return new WaitForSeconds(2);
        OnSuccessMiniGame();
    }
    IEnumerator ResetChannel()
    {
        _miniGame2State.isControllerAvailable = false;
        yield return new WaitForSeconds(0.5f);
        _miniGame2State.channel = "";
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


    public void OnClick(int i) 
    {
        _soundManager.PlayEffect1(_clickSound);
        OnAddNumber(i); 
    }
}
