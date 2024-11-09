using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1MiniGame1 : MiniGameManager
{
    public override void StartMiniGame()
    {
        Instantiate(this.gameObject);
    }
    public override void EndMiniGame()
    {
        Destroy(this.gameObject);
    }
}
