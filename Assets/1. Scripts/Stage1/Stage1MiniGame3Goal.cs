using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class Stage1MiniGame3Goal : MonoBehaviour
{
    [SerializeField] private Stage1MiniGame3StateSO _miniGame3State;

    SoundManager soundManager;
    Animator anim;

    [Header("Sound")]
    [SerializeField] AudioClip sound_AH;
    [SerializeField] AudioClip sound_UMUL;
    [SerializeField] AudioClip sound_Reject;
    bool isUMUL = false;
    
    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        anim = GetComponent<Animator>();

        _miniGame3State.isMouseOpen = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _miniGame3State.isBabyActive)
        {
            if (isUMUL) isUMUL = false;
            _miniGame3State.isMouseOpen = true;
            PlayAH();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _miniGame3State.isMouseOpen = false;
            if (isUMUL)
            {
                isUMUL = false;
            }
            else
            {
                anim.Play("UM");
            }
        }
    }
    private void OnDestroy()
    {
        soundManager.StopEffect1();
    }
    public void PlayEat()
    {
        isUMUL = true;
        anim.Play("UMUL");
        soundManager.StopEffect1();
        soundManager.PlayEffect1(sound_UMUL);
    }
    void PlayAH()
    {
        anim.Play("AH");
        soundManager.StopEffect1();
        soundManager.PlayEffect1(sound_AH);
    }
    public void PlayReject()
    {
        soundManager.StopEffect1();
        soundManager.PlayEffect1(sound_Reject);
    }
}
