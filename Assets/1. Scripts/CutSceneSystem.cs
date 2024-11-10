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
    [SerializeField] protected Image cutSceneBase;
    [SerializeField] protected Sprite[] cutScene;
    [SerializeField] protected Transform dialogParents;
    [SerializeField] protected GameObject dialogBackground;
    [SerializeField] protected CutSceneDialog[] cutSceneDialogs;
    [SerializeField] protected GameObject dialogUIPrefab;

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
    protected virtual async UniTask CutScene(int gid)
    {
        audioSource.Play();
        var cutSceneIndex = 0;
        var diaLogIndex = 0;

        // 시작
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

        StartFadeOut();
        RightFade.instance.FadeOut();
        await LeftFade.instance.FadeOut();

        cutSceneBase.gameObject.SetActive(false);
        dialogParents.gameObject.SetActive(false);

        RightFade.instance.FadeIn();
        await LeftFade.instance.FadeIn();
    }
    //////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////

    // 페이드아웃 시간 (초)
    public float fadeOutDuration = 2.0f;
    protected AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // 페이드아웃 시작하는 함수
    public void StartFadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = audioSource.volume;
        float currentTime = 0;

        while (currentTime < fadeOutDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0, currentTime / fadeOutDuration);
            yield return null;
        }

        // 페이드아웃 완료 후 오디오 정지
        audioSource.Stop();
    }
}
