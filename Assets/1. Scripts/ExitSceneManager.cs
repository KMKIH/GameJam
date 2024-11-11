using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitSceneManager : MonoBehaviour
{
    private async void Start()
    {
        // 게임 시작 시 Fade In
        FullFade.instance.FadeIn();
    }
    public async void Exit()
    {
        await FullFade.instance.FadeOutAsync();
#if UNITY_EDITOR
        // 에디터에서 실행 중일 때
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 빌드된 게임에서 실행 중일 때
            Application.Quit();
#endif
    }
    public async void Title()
    {
        await FullFade.instance.FadeOutAsync();
        SceneManager.LoadScene("StartScene");
    }
}
