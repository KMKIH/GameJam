using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTarget : MonoBehaviour
{
    [SerializeField] private GameStateSO _gameState;
    [SerializeField] private MiniGameManager _miniGameManager;

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (FindObjectOfType<StageSceneManager>().ClearList[_miniGameManager.gid] == true) return;
        if (FindObjectOfType<StageSceneManager>().ActiveList[_miniGameManager.gid] == false) return;

        if (collision.gameObject.tag == "Player" && // �÷��̾� üũ
            (_gameState.targetObject == transform.parent.gameObject || _gameState.targetObject == gameObject) && // �ش� ������Ʈ���� üũ
            _gameState.playerState == PlayerState.FocusLeft) // �÷��̾� ���� üũ
        {
            await RightFade.instance.FadeOutAsync(0.2f);
            _gameState.playerState = PlayerState.MiniGame;
            _gameState.targetMiniGame = _miniGameManager;
            _gameState.StartMiniGame();
            RightFade.instance.FadeIn(0.5f);
        }
    }
}
