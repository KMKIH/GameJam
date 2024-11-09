using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameSceneManager : MonoBehaviour
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
        // TODO: PlayerPref에 데이터를 이용해 저장한다
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
