using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightFade : MonoBehaviour
{
    Image image;
    public static RightFade instance;
    private void Awake()
    {
        instance = this;
    }

    public void FadeIn()
    {
        // image.DOFade();
    }
    public void FadeOut()
    {
        // image.DOFade();
    }
}
