using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour {
    //private BoxCollider2D dusmanCollider;
    private BoxCollider2D characterCollider;
    [SerializeField]
    private BoxCollider2D stepCollider;
    [SerializeField]
    private BoxCollider2D stepTrigger;

    private void Start()
    {
        characterCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        
        Physics2D.IgnoreCollision(stepCollider, stepTrigger, true);
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(stepCollider, characterCollider, true);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(stepCollider, characterCollider, false);
        }
       
    }
}
