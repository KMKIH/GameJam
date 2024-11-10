using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

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
        image.DOFade(1, 0);
        image.DOFade(0, 1);
    }
    public async UniTask FadeInAsync()
    {
        image.DOFade(1,0);
        image.DOFade(0,1);
        await UniTask.WaitForSeconds(1f);
    }
    public void FadeOut()
    {
        image.DOFade(0, 0);
        image.DOFade(1, 1);
    }
    public async UniTask FadeOutAsync()
    {
        image.DOFade(0, 0);
        image.DOFade(1, 1);
        await UniTask.WaitForSeconds(1f);
    }
}
