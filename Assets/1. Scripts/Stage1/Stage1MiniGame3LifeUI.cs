using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1MiniGame3LifeUI : MonoBehaviour
{
    public int life;
    [SerializeField] private Stage1MiniGame3StateSO _miniGame3State;
    private Image _image;

    [SerializeField] Sprite h;
    [SerializeField] Sprite x;


    void Start()
    {
        _image = GetComponent<Image>();
    }
    void Update()
    {
        if(_miniGame3State.life >= life)
        {
            _image.sprite = h;
        }
        else
        {
            _image.sprite = x;
        }
    }
}
