using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1MiniGame1 : MiniGameManager
{
    private int count = 0;
    private int countSuccess = 0;

    static GameObject realObject;

    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            if(count == 4)
            {
                if (countSuccess == 4)
                {
                    // 성공에 대한 처리
                    OnSuccessMiniGame();
                }
                else
                {
                    FindObjectOfType<GameManager>().TurnRestartPopUp(true);
                }
            }
        }
    }
    public int CountSuccess
    {
        get { return countSuccess; }
        set
        {
            countSuccess = value;
        }
    }
    public override void StartMiniGame()
    {
        realObject = Instantiate(this.gameObject);
    }
    public override void EndMiniGame()
    {
        if(realObject != null && realObject.activeSelf)
            Destroy(realObject);
    }
}
