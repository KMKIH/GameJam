using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingPortal : MonoBehaviour
{
    public string sceneName;
    [SerializeField] AudioClip cutSceneBgm;
    [SerializeField] Image _mapCover;
    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            await FindObjectOfType<CutSceneSystem>().StartCutScene(1);
            _mapCover.gameObject.SetActive(true);
            await FullFade.instance.FadeOutAsync();

            SceneManager.LoadScene(sceneName);
        }
    }
}
