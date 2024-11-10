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
    [SerializeField] Image cutSceneBase;
    [SerializeField] Sprite[] cutScene;
    [SerializeField] Transform dialogParents;
    [SerializeField] GameObject dialogBackground;
    [SerializeField] CutSceneDialog[] cutSceneDialogs;
    [SerializeField] GameObject dialogUIPrefab;

    public async UniTask StartCutScene(int gid)
    {
        _gameState.playerState = PlayerState.CutScene;

        cutSceneBase.gameObject.SetActive(true);
        dialogParents.gameObject.SetActive(true);
        dialogBackground.SetActive(true);

        await UniTask.WhenAll(CutScene(gid));

        dialogBackground.SetActive(false);
        dialogParents.gameObject.SetActive(false);
        cutSceneBase.gameObject.SetActive(false);

        _gameState.playerState = PlayerState.FocusLeft;
    }
    async UniTask CutScene(int gid)
    {
        var cutSceneIndex = 0;
        var diaLogIndex = 0;

        // Ω√¿€
        while (true)
        {
            await UniTask.NextFrame();
            if (Input.GetMouseButtonDown(0))
            {
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
            if(cutSceneIndex == cutSceneDialogs.Length)
            {
                break;
            }
            cutSceneBase.sprite = cutScene[cutSceneIndex];
        }

            RightFade.instance.FadeOut();
        await LeftFade.instance.FadeOut();

        cutSceneBase.gameObject.SetActive(false);
        dialogParents.gameObject.SetActive(false);

        RightFade.instance.FadeIn();
        await LeftFade.instance.FadeIn();
    }
}
