using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [Header("GameState")]
    [SerializeField] private GameStateSO _gameState;

    [Header("Pop Up UI (Exit)")]
    [SerializeField] GameObject exitPopUp;
    [SerializeField] Button exitBtn;
    [SerializeField] Button closeBtn;

    [Header("Pop Up UI (Restart)")]
    [SerializeField] GameObject restartPopUp;
    [SerializeField] Button restartBtn;

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

        // UI 연결
        exitPopUp.gameObject.SetActive(false);
        exitBtn.onClick.AddListener(OnClickExit);
        closeBtn.onClick.AddListener(OnClickCancel);

        restartPopUp.gameObject.SetActive(false);
        restartBtn.onClick.AddListener(OnClickRestart);
    }
    public void TurnExitPopUp(bool on)
    {
        if (restartPopUp.activeSelf) return;
        if(on)_gameState.MiniGameState = MiniGameState.Pause;
        exitPopUp.SetActive(on);
    }
    public void TurnRestartPopUp(bool on)
    {
        if (exitPopUp.activeSelf) return;
        if (on) _gameState.MiniGameState = MiniGameState.Pause;
        restartPopUp.SetActive(on);
    }


    // 나가기버튼 클릭
    void OnClickExit()
    {
        _gameState.MiniGameState = MiniGameState.End;
        _gameState.EndMiniGame();
        TurnExitPopUp(false);
    }
    // 취소버튼 클릭
    void OnClickCancel()
    {
        _gameState.MiniGameState = MiniGameState.OnGoing;
        TurnExitPopUp(false);
    }
    // 다시하기버튼 클릭
    void OnClickRestart()
    {
        _gameState.RestartMiniGame();
        TurnRestartPopUp(false);
    }
}
