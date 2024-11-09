using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class Stage1SceneManager : StageSceneManager
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

        // Event ����
        _gameState.OnMiniGameStateChanged += CheckClearState;
        _gameState.OnMiniGameStateChanged += ActiveObject;

    }
    async void NewGame()
    {
        await FindObjectOfType<CutSceneSystem>().StartCutScene(1);
    }
    void Resume()
    {
        // TODO: PlayerPref�� ��
    }

    //////////////////////////////////////////
    // Event
    async void CheckClearState(int gid, MiniGameState state)
    {
        if(state == MiniGameState.Success)
        {
            // �ش��ϴ� ������Ʈ �÷�������
            ClearList[gid] = true;
            for (int i = 0; i < 3; i++)
            {
                if (ClearList[i])
                {
                    objectImages[i].sprite = clearSprites[i];
                }
            }

            // 1�� �Ŀ� Ŭ���� üũ
            await UniTask.WaitForSeconds(1);

            if (clearList[0] && clearList[1] && clearList[2])
            {
                // ���̵� �ƿ� ����
                RightFade.instance.FadeOut();
                await LeftFade.instance.FadeOut();

                // ���� �������� �Ѿ��
                SceneManager.LoadScene("Stage2");
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
