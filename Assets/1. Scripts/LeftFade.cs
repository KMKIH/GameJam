using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public class LeftFade : MonoBehaviour
{
    Image image;
    public static LeftFade instance;
    private void Awake()
    {
        instance = this;
        image = GetComponent<Image>();
        image.DOFade(0, 0);
    }

    public async UniTask FadeIn()
    {
        image.DOFade(1, 0);
        image.DOFade(0, 1);
        await UniTask.WaitForSeconds(1f);
    }
    public async UniTask FadeOut()
    {
        image.DOFade(0, 0);
        image.DOFade(1, 1);
        await UniTask.WaitForSeconds(1f);
    }
}
