using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneManager : MonoBehaviour
{
    [SerializeField] AudioClip bgm;
    private void Start()
    {
        // 게임 시작 시 Fade In
        FullFade.instance.FadeIn();

        _ = GetComponent<SoundManager>().PlayWithFadeOut(bgm,1,true); 
    }
}
