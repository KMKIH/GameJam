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
        image = GetComponent<Image>();
        image.DOFade(0, 0);
    }

    public void FadeIn()
    {
        image.DOFade(1,0);
        image.DOFade(0,1);
    }
    public void FadeOut()
    {
        image.DOFade(0, 0);
        image.DOFade(1, 1);
    }
}
