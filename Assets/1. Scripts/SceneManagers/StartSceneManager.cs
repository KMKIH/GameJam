using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetInt("NewGaem", 1);
    }
    public void SceneLoaderWithSave(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetInt("NewGaem", 0);
    }
}
