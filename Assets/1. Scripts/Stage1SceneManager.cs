using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class Stage1SceneManager : StageSceneManager
{
    [Header("Obect")]
    [SerializeField] Portal portal;

    [Header("Audio")]
    [SerializeField] AudioClip bgm;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        // 게임 시작 시 Fade In
        FullFade.instance.FadeIn();

        // Start Game
        StartCutScene();

        // 값 설정
        ActiveList[0] = false;
        ActiveList[1] = true;
        ActiveList[2] = true;
        for (int i = 0; i < 3; i++)
        {
            if (activeList[i] == false) objectImages[i].sprite = inActiveSprites[i];
            else objectImages[i].sprite = activeSprites[i];
        }
        portal.gameObject.SetActive(false);

        // Event 연결
        _gameState.OnMiniGameStateChanged += CheckClearState;
        _gameState.OnMiniGameStateChanged += ActiveObject;
    }
    async void StartCutScene()
    {
        await FindObjectOfType<CutSceneSystem>().StartCutScene(1);

        LeftFade.instance.FadeOut(0.3f);
        await RightFade.instance.FadeOutAsync(0.3f);

        LeftFade.instance.FadeIn();
        RightFade.instance.FadeIn();
        // 컷씬이 종료되면 배경음을 재생한다
        _ = GetComponent<SoundManager>().PlayWithFadeOut(bgm,0.2f,true);
    }

    //////////////////////////////////////////
    // Event
    async void CheckClearState(int gid, MiniGameState state)
    {
        if(state == MiniGameState.Success)
        {
            // 해당하는 오브젝트 컬러입히기
            clearList[gid] = true;
            objectImages[gid].sprite = clearSprites[gid];

            // 1초 후에 클리어 체크
            await UniTask.WaitForSeconds(1);

            if (clearList[0] && clearList[1] && clearList[2])
            {
                // 페이드 아웃 이후
                RightFade.instance.FadeOut();
                await LeftFade.instance.FadeOutAsync();

                portal.gameObject.SetActive(true);

                RightFade.instance.FadeIn();
                LeftFade.instance.FadeIn();
            }
        }
    }
    void ActiveObject(int gid, MiniGameState state)
    {
        if (clearList[1] && clearList[2])
        {
            ActiveList[0] = true;
            if (clearList[0] == false)
                objectImages[0].sprite = activeSprites[0];
        }
    }
}
