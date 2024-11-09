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

        // Event ����
        _gameState.OnMiniGameStateChanged += CheckClearState;
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
    void CheckClearState(int gid, MiniGameState state)
    {
        if (state == MiniGameState.Success)
        {
            // �ش��ϴ� ������Ʈ �÷�������
            clearList[gid] = true;

            // 0.5�� �Ŀ� Ŭ���� üũ
            if (clearList[0] && clearList[1] && clearList[2])
            {
                // ���̵� �ƿ� ����

                // ���� �������� �Ѿ��
                SceneManager.LoadScene("Stage3");
            }

            ///////////////////////////////////////////
            // ������Ʈ ����
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
