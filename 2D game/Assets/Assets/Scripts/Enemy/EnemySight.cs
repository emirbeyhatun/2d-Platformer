using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

    [SerializeField]
    private EnemyWalk target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            target.Target = collision.gameObject;
            
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            target.Target = null;
        }
    }

   
}
