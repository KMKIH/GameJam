using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTarget : MonoBehaviour
{
    [SerializeField] private GameStateSO _gameState;
    [SerializeField] private MiniGameManager _miniGameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FindObjectOfType<StageSceneManager>().ClearList[_miniGameManager.gid] == true) return;

        if (collision.gameObject.tag == "Player" && _gameState.targetObject == gameObject)
        {
            _gameState.playerState = PlayerState.MiniGame;
            _gameState.targetMiniGame = _miniGameManager;
            _gameState.StartMiniGame();
        }
    }
}
