using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class CutSceneSystem : MonoBehaviour
{
    // Flags
    bool isCutsceneActing = false;

    // Cutscene
    [SerializeField]
    Animator cutSceneAnimator;
    [SerializeField]
    Transform dialogParents;
    [SerializeField]
    GameObject dialogBackground;
    [SerializeField]
    CutSceneDialog[] cutSceneDialogs;
    [SerializeField]
    GameObject dialogUIPrefab;

    public async UniTask StartCutScene(int gid)
    {
        GameManager.instance.playerState = PlayerState.CutScene;

        cutSceneAnimator.gameObject.SetActive(true);
        dialogBackground.SetActive(true);
        isCutsceneActing = false;

        await UniTask.WhenAll(CutScene(gid),CutSceneDialog(gid));

        isCutsceneActing = true;
        dialogBackground.SetActive(false);
        cutSceneAnimator.gameObject.SetActive(false);

        GameManager.instance.playerState = PlayerState.FocusLeft;
    }
    async UniTask CutScene(int gid)
    {
        // TODO: gid에 맞는 애니메이션 실행
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
