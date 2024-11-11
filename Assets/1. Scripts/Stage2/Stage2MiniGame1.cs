using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

public class Stage2MiniGame1 : MiniGameManager
{
    [SerializeField] Animator anim;
    [Header("Figure")]
    [SerializeField] Slider slider;
    [SerializeField] float gaze;

    [Header("for Debug")]
    [SerializeField] public bool isOnTheMilk;
    [SerializeField] public bool isUnderTheDesk;
    [Serializable]
    enum FriendState
    {
        None, Idle, Ready, LookBack
    }
    [SerializeField]
    FriendState friendState = FriendState.None;

    // Play
    [Header("Figure")]
    [SerializeField] float readyTime = 1f;
    [SerializeField] float idleMinTime = 3f;
    [SerializeField] float idleMaxTime = 8f;
    float curTime;
    float startTime;
    float idleTime;

    [Header("Sound")]
    [SerializeField] AudioClip turn;
    SoundManager soundManager;
    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();

        startTime = Time.time;
        idleTime = UnityEngine.Random.Range(idleMinTime, idleMaxTime);
    }

    private void Update()
    {
        if (_gameState.MiniGameState != MiniGameState.OnGoing) return;
        curTime = Time.time;

        // Clear
        if (gaze >= 100)
        {
            Debug.Log("¿Ï·á!");
            OnSuccessMiniGame();
            return;
        }

        // Play
        if (isOnTheMilk)
        {
            gaze += Time.deltaTime*10;
            slider.value = gaze / 100;
        }

        // Friend
        if (curTime - startTime > idleTime + readyTime*2)
        {
            friendState = FriendState.None;
            startTime = curTime;
        }
        else if (curTime - startTime > idleTime + readyTime)
        {
            if (friendState == FriendState.Ready)
            {
                friendState = FriendState.LookBack;
                anim.Play("LookBack");
            }
            if (isUnderTheDesk == false)
            {
                Debug.Log("°É·È´Ù!");
                FindObjectOfType<GameManager>().TurnRestartPopUp(true);
            }
        }
        else if (curTime - startTime > idleTime)
        {
            if (friendState == FriendState.Idle)
            {
                soundManager.PlayEffect2(turn);
                friendState = FriendState.Ready;
                anim.Play("Ready");
            }
        }
        else if (curTime - startTime < idleTime)
        {
            if (friendState == FriendState.None)
            {
                friendState = FriendState.Idle;
                anim.Play("Front");
            }
        }
    }
}
