using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectSorting : MonoBehaviour
{
    SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sr.sortingOrder = 1;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        sr.sortingOrder = 0;
    }
}
