using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectSorting : MonoBehaviour
{
    [SerializeField]SpriteRenderer sr;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            sr.sortingOrder = 1;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            sr.sortingOrder = 0;
    }
}
