using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class CutSceneSystem : MonoBehaviour
{
    // gameState
    [SerializeField] private GameStateSO _gameState;

    // Cutscene
    [SerializeField] Animator cutSceneAnimator;
    [SerializeField] Transform dialogParents;
    [SerializeField] GameObject dialogBackground;
    [SerializeField] CutSceneDialog[] cutSceneDialogs;
    [SerializeField] GameObject dialogUIPrefab;

    public async UniTask StartCutScene(int gid)
    {
        _gameState.playerState = PlayerState.CutScene;

        cutSceneAnimator.gameObject.SetActive(true);
        dialogBackground.SetActive(true);

        await UniTask.WhenAll(CutScene(gid),CutSceneDialog(gid));

        dialogBackground.SetActive(false);
        cutSceneAnimator.gameObject.SetActive(false);

        _gameState.playerState = PlayerState.FocusLeft;
    }
    async UniTask CutScene(int gid)
    {
        cutSceneAnimator.Play("CutScene" + gid);
        while (true)
        {
            await UniTask.NextFrame();
            if (cutSceneAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
            {
                break;
            }
        }
    }
    async UniTask CutSceneDialog(int gid)
    {
        var s = Time.time;
        var curTime = Time.time;
        var dialogs = cutSceneDialogs.FirstOrDefault(x => x.gid == gid).dialogs;
        foreach (var dialog in dialogs)
        {
            curTime = Time.time;
            var startTime = dialog.startTime;
            while (s + startTime > curTime)
            {
                curTime = Time.time;
                await UniTask.NextFrame();
            }
            var ui = Instantiate(dialogUIPrefab, dialogParents);
            ui.GetComponentInChildren<Text>().text = dialog.text;
        }
    }
}
