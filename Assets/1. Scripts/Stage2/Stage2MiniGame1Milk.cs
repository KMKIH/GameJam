using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2MiniGame1Milk : MonoBehaviour
{
    Stage2MiniGame1 miniGameManager;
    private void Awake()
    {
        miniGameManager = FindObjectOfType<Stage2MiniGame1>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Stage2MiniGame1Jeti jeti))
        {
            miniGameManager.isOnTheMilk = true;
            jeti.GetComponent<Animator>().Play("Put");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Stage2MiniGame1Jeti jeti))
        {
            miniGameManager.isOnTheMilk = false;
            jeti.GetComponent<Animator>().Play("Idle");
        }
    }
}
