using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerPrefs.SetInt("NewGaem", 1); // 새게임이다
            LeftFade.instance.FadeOut();
            RightFade.instance.FadeOut();
            SceneManager.LoadScene("Stage1");
        }
    }
}