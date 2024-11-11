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
    public virtual void EndMiniGame()
    {
        if (realObject != null && realObject.activeSelf)
        {
            RightFade.instance.FadeIn(1);
            Destroy(realObject);
        }
    }
    public async void OnSuccessMiniGame()
    {
        _gameState.MiniGameState = MiniGameState.Success;

        // 성공 이미지 띄우기
        await RightFade.instance.FadeOutAsync(1, "Clear");
        EndMiniGame();
    }
}
