﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private GameStateSO _gameState;

    private void Awake()
    {
        instance = this;
        /*
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        */
    }

    private void Start()
    {
        _gameState.ResetGameState();
    }
    public void RequestFocusLeft()
    {
        if(_gameState.playerState == PlayerState.MiniGame) // 미니게임인 경우에만 요청 가능
        {
            Debug.Log("Request Focus Left");
            // TODO: 팝업 UI ON
        }
    }
    
    // 취소버튼 클릭
    public void OnClickCancel()
    {

    }
    // 다시하기버튼 클릭
    public void OnClickRestart()
    {

    }
    // 나가기버튼 클릭
    public void OnClickExit()
    {

    }
}
