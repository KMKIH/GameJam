using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Stage1/Mini Game 2 State", fileName = "Mini Game 2 State")]
public class Stage1MiniGame2StateSO : ScriptableObject
{
    public string channel;
    public bool isControllerAvailable;
    public bool isChannelChanged;

    public void ResetState()
    {
        channel = "";
        isControllerAvailable = true;
        isChannelChanged = false;
    }
}
