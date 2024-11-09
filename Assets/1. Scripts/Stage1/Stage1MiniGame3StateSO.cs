using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "State/Stage1/Mini Game 3 State", fileName = "Mini Game 3 State")]
public class Stage1MiniGame3StateSO : ScriptableObject
{
    public int life;
    public bool isMouseOpen;
    public bool isReverse;
    public bool isBabyActive;
    public int index;
    public List<GameObject> foods;
    public void ResetState()
    {
        life = 0;
        isMouseOpen = true;
        isReverse = false;
        isBabyActive = true;
        index = 0;
    }
}
