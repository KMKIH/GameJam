using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class Stage1SceneManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] GameStateSO _gameState;
    [SerializeField] bool[] clearList = { false, false, false};

    [Header("Objects")]
    [SerializeField] SpriteRenderer[] objectImages;
    [SerializeField] Sprite[] clearSprites;
    public bool[] ClearList
    {
        get { return clearList; }
    }
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
        if(state == MiniGameState.Success)
        {
            // �ش��ϴ� ������Ʈ �÷�������
            clearList[gid] = true;

            // 0.5�� �Ŀ� Ŭ���� üũ
            if (clearList[0] && clearList[1] && clearList[2])
            {
                // ���̵� �ƿ� ����

                // ���� �������� �Ѿ��
                SceneManager.LoadScene("Stage2");
            }

            ///////////////////////////////////////////
            // ������Ʈ ����
            for(int i = 0; i < 3; i++)
            {
                if (clearList[i])
                {
                    objectImages[i].sprite = clearSprites[i];
                }
            }
        }
    }
}
