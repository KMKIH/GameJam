using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dgs : MonoBehaviour
{
    // Start is called before the first frame update
   async void Start()
    {

        await FindObjectOfType<CutSceneSystem>().StartCutScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
