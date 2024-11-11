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
        // ���� ���� �� Fade In
        FullFade.instance.FadeIn();

        // Start Game
        StartCutScene();

        // �� ����
        ActiveList[0] = false;
        ActiveList[1] = true;
        ActiveList[2] = true;
        for (int i = 0; i < 3; i++)
        {
            if (activeList[i] == false) objectImages[i].sprite = inActiveSprites[i];
            else objectImages[i].sprite = activeSprites[i];
        }
        portal.gameObject.SetActive(false);

        // Event ����
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
        // �ƾ��� ����Ǹ� ������� ����Ѵ�
        _ = GetComponent<SoundManager>().PlayWithFadeOut(bgm,0.2f,true);
    }

    //////////////////////////////////////////
    // Event
    async void CheckClearState(int gid, MiniGameState state)
    {
        if(state == MiniGameState.Success)
        {
            // �ش��ϴ� ������Ʈ �÷�������
            clearList[gid] = true;
            objectImages[gid].sprite = clearSprites[gid];

            // 1�� �Ŀ� Ŭ���� üũ
            await UniTask.WaitForSeconds(1);

            if (clearList[0] && clearList[1] && clearList[2])
            {
                // ���̵� �ƿ� ����
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
