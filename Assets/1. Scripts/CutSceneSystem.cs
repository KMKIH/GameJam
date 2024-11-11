using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

// 컷씬에 맞춰 음악까지 진행
public class CutSceneSystem : MonoBehaviour
{
    // GameState
    [SerializeField] private GameStateSO _gameState;

    // Cutscene
    [Header("CutScene")]
    [SerializeField] protected GameObject cutSceneObject;
    [SerializeField] protected Image cutSceneBase;
    [SerializeField] protected Sprite[] cutScene; // 추후 여러 컷씬을 넣는 경우 2차원배열이든 Dictionary로 변경해야한다
    [Header("Dialog")]
    [SerializeField] protected Transform dialogParents;
    [SerializeField] protected GameObject dialogBackground;
    [SerializeField] protected CutSceneDialog[] cutSceneDialogs; // 추후 여러 컷씬을 넣는 경우 2차원배열이든 Dictionary로 변경해야한다
    [SerializeField] protected GameObject dialogUIPrefab;
    [Header("Audio")]
    [SerializeField] AudioClip[] audioClips;
    SoundManager soundManager;
    private void Awake()
    {
        soundManager = GetComponent<SoundManager>();
    }
    public async UniTask StartCutScene(int gid)
    {
        _gameState.playerState = PlayerState.CutScene;

        // 컷씬 관련 오브젝트 ON
        cutSceneObject.SetActive(true);
        dialogParents.gameObject.SetActive(true);
        dialogBackground.SetActive(true);

        await UniTask.WhenAll(CutScene(gid));

        // 컷씬 관련 오브젝트 OFF
        dialogBackground.SetActive(false);
        dialogParents.gameObject.SetActive(false);
        cutSceneObject.SetActive(false);

        _gameState.playerState = PlayerState.FocusLeft;
    }
    protected virtual async UniTask CutScene(int gid)
    {
        var cutSceneIndex = 0;
        var diaLogIndex = 0;

        // 시작
        LeftFade.instance.FadeOut(0);
        while (true)
        {
            await UniTask.NextFrame();
            if (Input.GetMouseButtonDown(0))
            {
                LeftFade.instance.FadeIn();
                cutSceneBase.sprite = cutScene[cutSceneIndex];
                if (cutSceneIndex < audioClips.Length && audioClips[cutSceneIndex] != null)
                {
                    _ = soundManager.PlayWithFadeOut(audioClips[cutSceneIndex]);
                }
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

            // CutScene 변경
            if(cutSceneIndex == cutSceneDialogs.Length) break;
            cutSceneBase.sprite = cutScene[cutSceneIndex];
            if (cutSceneIndex < audioClips.Length && audioClips[cutSceneIndex] != null)
            {
                _ = soundManager.PlayWithFadeOut(audioClips[cutSceneIndex]);
            }
        }
    }
}
