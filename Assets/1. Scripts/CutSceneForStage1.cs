using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class CutSceneForStage1 : CutSceneSystem
{
    [SerializeField] AudioClip[] audioClips;
    protected override async UniTask CutScene(int gid)
    {
        var cutSceneIndex = 0;
        var diaLogIndex = 0;

        // Ω√¿€
        while (true)
        {
            await UniTask.NextFrame();
            if (Input.GetMouseButtonDown(0))
            {
                if (cutSceneIndex <= 2)
                {
                    audioSource.clip = audioClips[cutSceneIndex];
                    audioSource.Play();
                }
                cutSceneBase.sprite = cutScene[cutSceneIndex];
                break;
            }
        }

        while (true) // CutScene
        {
            diaLogIndex = 0;
            await UniTask.NextFrame();
            while (true) // Dialog
            {
                await UniTask.NextFrame();
                if (Input.GetMouseButtonDown(0) && diaLogIndex == cutSceneDialogs[cutSceneIndex].dialogs.Length)
                {
                    cutSceneIndex++;
                    break;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    var ui = Instantiate(dialogUIPrefab, dialogParents);
                    var text = cutSceneDialogs[cutSceneIndex].dialogs[diaLogIndex++];
                    ui.GetComponentInChildren<Text>().text = text;
                }
            }
            if (cutSceneIndex == cutSceneDialogs.Length)
            {
                break;
            }
            if (cutSceneIndex <= 2)
            {
                audioSource.clip = audioClips[cutSceneIndex];
                audioSource.Play();
            }
            cutSceneBase.sprite = cutScene[cutSceneIndex];
        }

        StartFadeOut();
        RightFade.instance.FadeOut();
        await LeftFade.instance.FadeOutAsync();

        cutSceneBase.gameObject.SetActive(false);
        dialogParents.gameObject.SetActive(false);

        RightFade.instance.FadeIn();
        await LeftFade.instance.FadeInAsync();

        audioSource.clip = audioClips[audioClips.Length-1];
        audioSource.Play();
    }
}
