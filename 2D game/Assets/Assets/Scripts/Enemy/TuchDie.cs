using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuchDie : MonoBehaviour {
    
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" )
        {
            
            StartCoroutine(WhenHealthDecrease.Ornek.DisableColliderForATime());
            

        }
    }

    
    
}
