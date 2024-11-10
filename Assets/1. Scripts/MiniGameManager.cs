using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class MiniGameManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] public GameStateSO _gameState;
    [Header("GID")]
    [SerializeField] public int gid;


    static GameObject realObject;
    public void StartMiniGame()
    {
        realObject = Instantiate(this.gameObject);
    }
    public async void EndMiniGame()
    {
        if (realObject != null && realObject.activeSelf)
        {
            await RightFade.instance.FadeOutAsync();
            Destroy(realObject);
            RightFade.instance.FadeIn();
        }
    }
    public async void OnSuccessMiniGame()
    {
        _gameState.MiniGameState = MiniGameState.Success;
        await UniTask.WaitForSeconds(1);
        EndMiniGame();
    }
}
