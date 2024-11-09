using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Stage2SceneManager : StageSceneManager
{
    private void Start()
    {
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

        // Event 연결
        _gameState.OnMiniGameStateChanged += CheckClearState;
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
    void CheckClearState(int gid, MiniGameState state)
    {
        if (state == MiniGameState.Success)
        {
            // 해당하는 오브젝트 컬러입히기
            clearList[gid] = true;

            // 0.5초 후에 클리어 체크
            if (clearList[0] && clearList[1] && clearList[2])
            {
                // 페이드 아웃 이후

                // 다음 스테이지 넘어가기
                SceneManager.LoadScene("Stage3");
            }

            ///////////////////////////////////////////
            // 오브젝트 변경
            for (int i = 0; i < 3; i++)
            {
                if (clearList[i])
                {
                    objectImages[i].sprite = clearSprites[i];
                }
            }
        }
    }
}
