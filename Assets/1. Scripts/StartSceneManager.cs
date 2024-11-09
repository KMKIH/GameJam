using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button continueButton;
    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        continueButton.onClick.AddListener(SceneLoaderWithSave);

        if(PlayerPrefs.HasKey("PlayerStage") == false)
        {
            continueButton.interactable = false;
        }
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("PlayerStage", 1); // 플레이어가 시작할 스테이지
        PlayerPrefs.SetInt("NewGaem", 1); // 새게임이다
        SceneManager.LoadScene("Stage1");
    }
    public void SceneLoaderWithSave()
    {
        PlayerPrefs.SetInt("NewGaem", 0); // 새게임이 아니다
        SceneManager.LoadScene("Stage"+PlayerPrefs.GetInt("PlayerStage"));
    }
}