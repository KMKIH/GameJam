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

    bool _isFadingOut = false;
    bool _isFadingIn = false;

    private void Awake()
    {
        instance = this;
        image = GetComponent<Image>();
        image.DOFade(0, 0);
    }

    public async UniTask FadeInAsync(float time = 1f)
    {
        if (_isFadingIn) return;
        _isFadingIn = true;
        image.DOFade(1, 0);
        image.DOFade(0, time);
        await UniTask.WaitForSeconds(time);
    }
    public void FadeIn(float time = 1f)
    {
        if (_isFadingIn) return;
        _isFadingIn = true;
        image.DOFade(1, 0);
        image.DOFade(0, time);
    }
    public async UniTask FadeOutAsync(float time = 1f)
    {
        if (_isFadingOut) return;
        _isFadingOut = true;
        image.DOFade(0, 0);
        image.DOFade(1, time);
        await UniTask.WaitForSeconds(time);
    }
    public void FadeOut(float time = 1f)
    {
        if (_isFadingOut) return;
        _isFadingOut = true;
        image.DOFade(0, 0);
        image.DOFade(1, time);
    }
}
