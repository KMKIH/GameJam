using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSceneManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] protected GameStateSO _gameState;
    [SerializeField] protected bool[] clearList = { false, false, false };
    [SerializeField] protected bool[] activeList = { false, false, false };

    [Header("Objects")]
    [SerializeField] protected SpriteRenderer[] objectImages;
    [SerializeField] protected Sprite[] clearSprites;
    [SerializeField] protected Sprite[] activeSprites;
    [SerializeField] protected Sprite[] inActiveSprites;
    
    public bool[] ClearList
    {
        get { return clearList; }
    }
    public bool[] ActiveList
    {
        get { return activeList; }
    }
}
