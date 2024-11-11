using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2MiniGame1Milk : MonoBehaviour
{
    Stage2MiniGame1 miniGameManager;
    [Header("Sound")]
    [SerializeField] AudioClip put;
    SoundManager soundManager;
    private void Awake()
    {
        miniGameManager = FindObjectOfType<Stage2MiniGame1>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Stage2MiniGame1Jeti jeti))
        {
            miniGameManager.isOnTheMilk = true;
            jeti.GetComponent<Animator>().Play("Put");
            soundManager.PlayEffect1(put,1,true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Stage2MiniGame1Jeti jeti))
        {
            miniGameManager.isOnTheMilk = false;
            jeti.GetComponent<Animator>().Play("Idle");
            soundManager.StopEffect1();
        }
    }
}
