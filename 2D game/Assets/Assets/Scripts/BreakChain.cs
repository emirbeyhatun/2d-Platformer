using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakChain : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Knife")
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
