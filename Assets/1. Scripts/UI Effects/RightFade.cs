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

    Text textObject;

    bool _isFadingOut = false;
    bool _isFadingIn = false;

    private void Awake()
    {
        instance = this;
        image = GetComponent<Image>();
        textObject = GetComponentInChildren<Text>();
        image.DOFade(0, 0);
    }

    public async UniTask FadeInAsync(float time = 1f)
    {
        textObject.text = "";

        if (_isFadingIn) return;
        _isFadingIn = true;

        image.DOFade(1, 0);
        image.DOFade(0, time);

        await UniTask.WaitForSeconds(time);

        _isFadingIn = false;
    }
    public void FadeIn(float time = 1f)
    {
        textObject.text = "";

        image.DOFade(1, 0);
        image.DOFade(0, time);
    }
    public async UniTask FadeOutAsync(float time = 1f, string text = "")
    {
        textObject.text = "";

        if (_isFadingOut) return;
        _isFadingOut = true;

        textObject.text = text;

        image.DOFade(0, 0);
        image.DOFade(1, time);

        await UniTask.WaitForSeconds(time);
        _isFadingOut = false;
    }
    public void FadeOut(float time = 1f)
    {
        textObject.text = "";

        image.DOFade(0, 0);
        image.DOFade(1, time);
    }
}
