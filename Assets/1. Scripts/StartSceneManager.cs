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
        PlayerPrefs.SetInt("PlayerStage", 1); // �÷��̾ ������ ��������
        PlayerPrefs.SetInt("NewGaem", 1); // �������̴�
        SceneManager.LoadScene("Stage1");
    }
    public void SceneLoaderWithSave()
    {
        PlayerPrefs.SetInt("NewGaem", 0); // �������� �ƴϴ�
        SceneManager.LoadScene("Stage"+PlayerPrefs.GetInt("PlayerStage"));
    }
}