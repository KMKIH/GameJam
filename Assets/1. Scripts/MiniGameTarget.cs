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

        if (collision.gameObject.tag == "Player" && // 플레이어 체크
            (_gameState.targetObject == transform.parent.gameObject || _gameState.targetObject == gameObject) && // 해당 오브젝트인지 체크
            _gameState.playerState == PlayerState.FocusLeft) // 플레이어 상태 체크
        {
            await RightFade.instance.FadeOutAsync(0.2f);
            _gameState.playerState = PlayerState.MiniGame;
            _gameState.targetMiniGame = _miniGameManager;
            _gameState.StartMiniGame();
            RightFade.instance.FadeIn(0.5f);
        }
    }
}
