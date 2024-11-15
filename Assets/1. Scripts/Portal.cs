using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneName;
    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            await FullFade.instance.FadeOutAsync();

            SceneManager.LoadScene(sceneName);
        }
    }
}
