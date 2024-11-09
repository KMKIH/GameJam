using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1MiniGame2Television : MonoBehaviour
{
    [SerializeField] private Stage1MiniGame2StateSO _miniGame2State;
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        Debug.Log(_miniGame2State.channel);
        if (_miniGame2State.channel == "175")
        {
            _image.color = new Color(0, 0, 1, 1);
            return;
        }
        if (_miniGame2State.isChannelChanged && 
            _miniGame2State.channel != "175")
        {
            _image.color = new Color(1, 0, 0, 1);
            return;
        }
    }
}
