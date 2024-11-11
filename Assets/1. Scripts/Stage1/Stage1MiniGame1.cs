using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1MiniGame1 : MiniGameManager
{
    private int count = 0;
    private int countSuccess = 0;
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


    [SerializeField] Text _countText;
    private void Update()
    {
        _countText.text = $"{count} / {4}";
    }
}
