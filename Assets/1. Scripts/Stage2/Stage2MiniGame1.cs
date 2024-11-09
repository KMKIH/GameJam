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
    [SerializeField] float readyTIme =0.5f;
    [SerializeField] float idleMinTime = 2f;
    [SerializeField] float idleMaxTime = 12f;

    [SerializeField] public bool isOnTheMilk;
    [SerializeField] public bool isUnderTheDesk;

    private CancellationTokenSource _cancellationTokenSource;
    [Serializable]
    enum FriendState
    {
        Idle, Ready, LookBack
    }
    [SerializeField]
    FriendState friendState = FriendState.Idle;
    private void Start()
    {
        // 취소 토큰 소스를 초기화하고 UniTask 실행
        _cancellationTokenSource = new CancellationTokenSource();
        StartGame();
    }

    private void Update()
    {
        if (_gameState.MiniGameState != MiniGameState.OnGoing) return;

        if (gaze >= 100)
        {
            Debug.Log("완료!");
            CancelTask();
            OnSuccessMiniGame();
        }
        if (isOnTheMilk)
        {
            gaze += Time.deltaTime*3;
            slider.value = gaze / 100;
        }

        // 상태 확인
        if(friendState == FriendState.LookBack && isUnderTheDesk == false)
        {
            Debug.Log("걸렸다!");
            CancelTask();
            FindObjectOfType<GameManager>().TurnRestartPopUp(true);
        }


    }
    private async void StartGame()
    {
        try
        {
            while (true)
            {
                friendState = FriendState.Idle;
                anim.Play("Front");
                var idleTime = UnityEngine.Random.Range(idleMinTime, idleMaxTime);
                await UniTask.WaitForSeconds(idleTime);
                

                friendState = FriendState.Ready;
                anim.Play("Ready");
                await UniTask.WaitForSeconds(readyTIme);

                friendState = FriendState.LookBack;
                anim.Play("LookBack");
                await UniTask.WaitForSeconds(readyTIme);
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("작업이 취소되었습니다.");
        }
    }

    private void CancelTask()
    {
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }
}
