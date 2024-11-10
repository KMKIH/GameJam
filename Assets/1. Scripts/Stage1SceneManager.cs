using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class Stage1SceneManager : StageSceneManager
{
    [SerializeField] Portal portal;
    private void Start()
    {
        portal.gameObject.SetActive(false);
        // Start Game
        switch (PlayerPrefs.GetInt("NewGame"))
        {
            case 0:
                NewGame();
                break;
            case 1:
                Resume();
                break;
        }

        // Set Active
        ActiveList[0] = true;
        ActiveList[1] = true;
        ActiveList[2] = false;

        for (int i = 0; i < 3; i++)
        {
            if (activeList[i] == false)
            {
                objectImages[i].sprite = inActiveSprites[i];
            }
            else
            {
                objectImages[i].sprite = activeSprites[i];
            }
        }

        // Event 연결
        _gameState.OnMiniGameStateChanged += CheckClearState;
        _gameState.OnMiniGameStateChanged += ActiveObject;

    }
    async void NewGame()
    {
        await FindObjectOfType<CutSceneSystem>().StartCutScene(1);
    }
    void Resume()
    {
        // TODO: PlayerPref에 따
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
                await LeftFade.instance.FadeOut();

                portal.gameObject.SetActive(true);

                RightFade.instance.FadeIn();
                LeftFade.instance.FadeIn();

                // 다음 스테이지 넘어가기
                // SceneManager.LoadScene("Stage2");
            }
        }
    }
    void ActiveObject(int gid, MiniGameState state)
    {
        if (clearList[0] && clearList[1])
        {
            ActiveList[2] = true;
            if (clearList[2] == false)
                objectImages[2].sprite = activeSprites[2];
        }
    }
}
