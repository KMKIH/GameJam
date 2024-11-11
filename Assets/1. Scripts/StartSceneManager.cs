using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] Text noticeText;
    private void Start()
    {
        Sequence blinkSequence = DOTween.Sequence();
        blinkSequence
            .Append(noticeText.DOFade(0, 1).SetEase(Ease.InSine))
            .Append(noticeText.DOFade(1, 1).SetEase(Ease.InSine));
        blinkSequence.SetLoops(-1);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameStart();
        }
    }
    async void GameStart()
    {
        // 페이드 아웃
        fadeImage.DOFade(1, 1);
        while (true)
        {
            await UniTask.NextFrame();
            if (fadeImage.color.a >= 1) break;
        }

        // 다음씬 이동
        SceneManager.LoadScene("Stage1");
    }
}