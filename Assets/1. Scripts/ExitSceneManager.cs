using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitSceneManager : MonoBehaviour
{
    private async void Start()
    {
        // ���� ���� �� Fade In
        FullFade.instance.FadeIn();
    }
    public async void Exit()
    {
        await FullFade.instance.FadeOutAsync();
#if UNITY_EDITOR
        // �����Ϳ��� ���� ���� ��
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // ����� ���ӿ��� ���� ���� ��
            Application.Quit();
#endif
    }
    public async void Title()
    {
        await FullFade.instance.FadeOutAsync();
        SceneManager.LoadScene("StartScene");
    }
}
