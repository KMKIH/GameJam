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

    // 나가기버튼 클릭
    public void OnClickExit()
    {

    }
    // 취소버튼 클릭
    public void OnClickCancel()
    {

    }
    // 다시하기버튼 클릭
    public void OnClickRestart()
    {

    }
}
