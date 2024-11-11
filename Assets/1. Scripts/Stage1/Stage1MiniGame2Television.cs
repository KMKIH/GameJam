using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1MiniGame2Television : MonoBehaviour
{
    [SerializeField] private Stage1MiniGame2StateSO _miniGame2State;
    private Image _image;
    SoundManager _soundManager;

    int _lastIndex = -1;
    int _spriteIndex = -1;

    [Header("View")]
    [SerializeField] Sprite successImage;
    [SerializeField] AudioClip success;
    [SerializeField] Sprite[] failedImage;
    [SerializeField] AudioClip[] failed;

    bool _isSuccess = false;

    void Start()
    {
        _image = GetComponent<Image>();
        _soundManager =FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        if (_miniGame2State.channel == "175" && _isSuccess == false)
        {
            _isSuccess = true;
            _image.sprite = successImage;
            _soundManager.PlayEffect2(success);
            return;
        }
        if (_miniGame2State.isChannelChanged && 
            _miniGame2State.channel != "175")
        {
            _miniGame2State.isChannelChanged = false;
            do
            {
                _spriteIndex = UnityEngine.Random.Range(0, failedImage.Length);
            }
            while (_lastIndex == _spriteIndex);
            _image.sprite = failedImage[_spriteIndex];
            _soundManager.PlayEffect2(failed[_spriteIndex]);
            return;
        }
    }
}
