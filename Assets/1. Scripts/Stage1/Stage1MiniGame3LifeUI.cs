using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1MiniGame3LifeUI : MonoBehaviour
{
    public int life;
    [SerializeField] private Stage1MiniGame3StateSO _miniGame3State;
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
    }
    void Update()
    {
        float transparency = _miniGame3State.life >= life ? 1f : 0.4f;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, transparency);
    }
}
