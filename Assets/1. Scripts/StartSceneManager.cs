using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class StartSceneManager : MonoBehaviour
{
    [SerializeField] Image title;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerPrefs.SetInt("NewGaem", 1); // 새게임이다
            title.DOFade(0, 1);
            SceneManager.LoadScene("Stage1");
        }
    }
}