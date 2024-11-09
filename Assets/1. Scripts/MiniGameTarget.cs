using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTarget : MonoBehaviour
{
    [SerializeField] private GameStateSO _gameState;
    [SerializeField] private MiniGameManager _miniGameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        Debug.Log(_gameState.targetObject);
        if (collision.gameObject.tag == "Player" && _gameState.targetObject == gameObject)
        {
            Debug.Log("Mini Game Start!");
            _gameState.playerState = PlayerState.MiniGame;
            _gameState.targetMiniGame = _miniGameManager;
            _gameState.StartMiniGame();
        }
    }
}
