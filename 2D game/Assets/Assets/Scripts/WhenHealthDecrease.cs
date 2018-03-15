using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenHealthDecrease : MonoBehaviour {
    //[SerializeField]
    //private Collider2D[] IgnoreColliderArray;
    private Collider2D character;
    private GameObject Player;
    private static WhenHealthDecrease ornek;
    public static WhenHealthDecrease Ornek
    {
        get
        {
            if (ornek == null)
            {
                ornek = GameObject.FindObjectOfType<WhenHealthDecrease>();

            }
            return ornek;
        }
    }
    private void Start()
    {
       
        Player = GameObject.FindGameObjectWithTag("Player");


    }
    
    void Blink()
    {
        if (Player.GetComponent<SpriteRenderer>().enabled == true)
            Player.GetComponent<SpriteRenderer>().enabled = false;
        else if (Player.GetComponent<SpriteRenderer>().enabled == false)
            Player.GetComponent<SpriteRenderer>().enabled = true;
    }
    public IEnumerator DisableColliderForATime()
    {
        
        GameObject.FindGameObjectWithTag("Player").gameObject.layer = LayerMask.NameToLayer("Untouchable");
        LifeController.Ornek.LifePoints -= 1;
        


        InvokeRepeating("Blink", 0, 0.2f);
       

        yield return new WaitForSeconds(2f);


        CancelInvoke("Blink");
        Player.GetComponent<SpriteRenderer>().enabled = true;//incase if last blink is not  true

        Player.gameObject.layer = LayerMask.NameToLayer("Player");
        
       
        
        
        



    }
}
