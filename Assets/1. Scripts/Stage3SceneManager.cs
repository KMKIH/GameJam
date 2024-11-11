using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3SceneManager : StageSceneManager
{
    [Header("Obect")]
    [SerializeField] Portal portal;

    [Header("Audio")]
    [SerializeField] AudioClip bgm;

    private void Start()
    {
        // ���� ���� �� Fade In
        FullFade.instance.FadeIn();

        // Start Game
        StartCutScene();

        // �� ����
        ActiveList[0] = true;
        ActiveList[1] = true;
        ActiveList[2] = false;
        for (int i = 0; i < 3; i++)
        {
            if (activeList[i] == false) objectImages[i].sprite = inActiveSprites[i];
            else objectImages[i].sprite = activeSprites[i];
        }
        portal.gameObject.SetActive(false);

        // Event ����
        _gameState.OnMiniGameStateChanged -= CheckClearState;
        _gameState.OnMiniGameStateChanged += CheckClearState;
        _gameState.OnMiniGameStateChanged -= ActiveObject;
        _gameState.OnMiniGameStateChanged += ActiveObject;
    }
    void OnDestroy()
    {
        _gameState.OnMiniGameStateChanged -= CheckClearState;
        _gameState.OnMiniGameStateChanged -= ActiveObject;
    }
    async void StartCutScene()
    {
        await FindObjectOfType<CutSceneSystem>().StartCutScene(1);

        LeftFade.instance.FadeOut(0.3f);
        await RightFade.instance.FadeOutAsync(0.3f);

        LeftFade.instance.FadeIn();
        RightFade.instance.FadeIn();
        // �ƾ��� ����Ǹ� ������� ����Ѵ�
        _ = GetComponent<SoundManager>().PlayWithFadeOut(bgm, 0.3f, true);
    }

    //////////////////////////////////////////
    // Event
    async void CheckClearState(int gid, MiniGameState state)
    {
        if (state == MiniGameState.Success)
        {
            // �ش��ϴ� ������Ʈ �÷�������
            ClearList[gid] = true;
            for (int i = 0; i < 3; i++)
            {
                if (clearList[i])
                {
                    objectImages[i].sprite = clearSprites[i];
                }
            }

            // 0.5�� �Ŀ� Ŭ���� üũ
            if (clearList[0] && clearList[1] && clearList[2])
            {
                // ���̵� �ƿ� ����
                RightFade.instance.FadeOut();
                await LeftFade.instance.FadeOutAsync();

                portal.gameObject.SetActive(true);

                RightFade.instance.FadeIn();
                await LeftFade.instance.FadeInAsync();
            }
        }
    }
    void ActiveObject(int gid, MiniGameState state)
    {
        if (clearList[0] && clearList[1])
        {
            ActiveList[2] = true;
            objectImages[2].sprite = activeSprites[2];
        }
    }
}
