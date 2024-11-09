using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTarget : MonoBehaviour
{
    [SerializeField] private GameStateSO _gameState;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && _gameState.targetObject == gameObject)
        {
            Debug.Log("Mini Game Start!");
            _gameState.playerState = PlayerState.MiniGame;
        }
    }
}
