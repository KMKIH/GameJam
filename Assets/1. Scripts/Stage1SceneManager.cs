using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Stage1SceneManager : MonoBehaviour
{
    // Flags
    bool isCutsceneActing = false;

    // Cutscene
    [SerializeField]
    Animator cutSceneAnimator;

    private void Start()
    {
        switch (PlayerPrefs.GetInt("NewGame"))
        {
            case 0:
                NewGame();
                break;
            case 1:
                Resume();
                break;
        }
    }
    async void NewGame()
    {
        isCutsceneActing = false;
        await UniTask.WhenAll(CutScene());
        isCutsceneActing = true;
        cutSceneAnimator.gameObject.SetActive(false);
    }
    void Resume()
    {
        // TODO: PlayerPref?? ??????? ????? ???????
    }

    async UniTask CutScene()
    {
        while (true)
        {
            await UniTask.NextFrame();
            if (cutSceneAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
            {
                break;
            }
        }
    }
}
