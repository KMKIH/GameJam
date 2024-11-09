using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1MiniGame2Television : MonoBehaviour
{
    [SerializeField] private Stage1MiniGame2StateSO _miniGame2State;
    private Image _image;

    [Header("Images")]
    [SerializeField] Sprite successImage;
    [SerializeField] Sprite[] failedImage;


    void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if (_miniGame2State.channel == "175")
        {
            _image.sprite = successImage;
            return;
        }
        if (_miniGame2State.isChannelChanged && 
            _miniGame2State.channel != "175")
        {
            var spriteIndex = UnityEngine.Random.Range(0, failedImage.Length);
            _image.sprite = failedImage[spriteIndex];
            return;
        }
    }
}
