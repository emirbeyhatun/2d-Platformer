using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour {
    [SerializeField]
    private GameObject Portal;
    [SerializeField]
    private GameObject whatToTeleport;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.gameObject.tag == whatToTeleport.tag)//what to teleport 
        {

           StartCoroutine(Wait());
            
            collision.transform.position = Portal.transform.position;
           
           

        }
    }

    
    public IEnumerator Wait()
    {
        Portal.layer = LayerMask.NameToLayer("IgnorePlayer");

        yield return new WaitForSeconds(1f);

        
        Portal.layer = LayerMask.NameToLayer("Ground");
    }
}
