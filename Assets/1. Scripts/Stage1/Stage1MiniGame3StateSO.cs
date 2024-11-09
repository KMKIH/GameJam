using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Stage1/MiniGame3State", fileName = "Mini Game 3 State")]
public class Stage1MiniGame3StateSO : ScriptableObject
{
    public int life;
    public bool isMouthOpen;
    public int index;
    public List<GameObject> foods;
    public void ResetState()
    {
        life = 0;
        isMouthOpen = false;
        index = 0;
    }
}
