using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSkyGate : MonoBehaviour {

    [SerializeField]
    private Animator openGate;
    [SerializeField] 
    private Sprite openSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" )
        {
            
            openGate.SetTrigger("open");
            gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;
        }
       
    }
}
