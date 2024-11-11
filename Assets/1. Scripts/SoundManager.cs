using Cysharp.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource_Bgm;
    private AudioSource audioSource_Effect1;
    private AudioSource audioSource_Effect2;
    private bool isFading = false;
    [SerializeField] float fadeOutDuration = 0.2f;
    private CancellationTokenSource cts;

    private void Awake()
    {
        audioSource_Bgm = GetComponents<AudioSource>()[0];
        audioSource_Effect1 = GetComponents<AudioSource>()[1];
        audioSource_Effect2 = GetComponents<AudioSource>()[2];
        cts = new CancellationTokenSource();
    }

    private void OnDestroy()
    {
        cts?.Cancel();
        cts?.Dispose();
    }
    public void PlayEffect1(AudioClip clip, float volumn = 1f, bool isLoop = false)
    {
        audioSource_Effect1.loop = isLoop;
        audioSource_Effect1.volume = volumn;
        audioSource_Effect1.clip = clip;
        audioSource_Effect1.Play();
    }
    public void StopEffect1()
    {
        audioSource_Effect1?.Stop();
    }
    public void PlayEffect2(AudioClip clip, float volumn = 1f, bool isLoop = false)
    {
        audioSource_Effect1.loop = isLoop;
        audioSource_Effect2.volume = volumn;
        audioSource_Effect2.clip = clip;
        audioSource_Effect2.Play();
    }
    public void StopEffect2()
    {
        audioSource_Effect2?.Stop();
    }
    public async UniTask PlayWithFadeOut(AudioClip clip, float volumn = 1f, bool isLoop = false)
    {
        audioSource_Bgm.loop = isLoop;
        audioSource_Bgm.volume = volumn;
        if (!audioSource_Bgm.isPlaying)
        {
            audioSource_Bgm.clip = clip;
            audioSource_Bgm.Play();
            return;
        }

        // 진행 중인 페이드 작업 취소
        cts.Cancel();
        cts = new CancellationTokenSource();

        await PlayWithFadeOut(clip, cts.Token);
    }

    private async UniTask PlayWithFadeOut(AudioClip newClip, CancellationToken token)
    {
        if (isFading) return;

        try
        {
            isFading = true;

            float startVolume = audioSource_Bgm.volume;
            float elapsedTime = 0;

            // 페이드아웃
            while (elapsedTime < fadeOutDuration)
            {
                if (token.IsCancellationRequested) return;

                elapsedTime += Time.deltaTime;
                audioSource_Bgm.volume = Mathf.Lerp(startVolume, 0, elapsedTime / fadeOutDuration);
                await UniTask.Yield(token);
            }

            // 새로운 클립 재생
            audioSource_Bgm.Stop();
            audioSource_Bgm.volume = startVolume;
            audioSource_Bgm.clip = newClip;
            audioSource_Bgm.Play();
        }
        finally
        {
            isFading = false;
        }
    }

    public async UniTask Stop()
    {
        if (!audioSource_Bgm.isPlaying) return;

        // 진행 중인 페이드 작업 취소
        cts.Cancel();
        cts = new CancellationTokenSource();

        await StopWithFadeOut(cts.Token);
    }

    private async UniTask StopWithFadeOut(CancellationToken token)
    {
        if (isFading) return;

        try
        {
            isFading = true;

            float startVolume = audioSource_Bgm.volume;
            float fadeOutDuration = 0.5f;
            float elapsedTime = 0;

            while (elapsedTime < fadeOutDuration)
            {
                if (token.IsCancellationRequested) return;

                elapsedTime += Time.deltaTime;
                audioSource_Bgm.volume = Mathf.Lerp(startVolume, 0, elapsedTime / fadeOutDuration);
                await UniTask.Yield(token);
            }

            audioSource_Bgm.Stop();
            audioSource_Bgm.volume = startVolume;
        }
        finally
        {
            isFading = false;
        }
    }
}