using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Knife : MonoBehaviour {

    [SerializeField]
    private float knifeSpeed;

    private Rigidbody2D myRigidbody;
    private Vector2 KnifeDirection;
    [SerializeField]
    private float amount;
    private float turn;
    
    
   
    private void Start()
    {
        TurnTheAxe();
      
    }
    private void Update()
    {
        transform.rotation=Quaternion.Euler(new Vector3(0, 0, turn));
        turn += amount;
    }
    private void TurnTheAxe()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        turn = -90f;
    }
    private void FixedUpdate()
    {
        myRigidbody.velocity = KnifeDirection * knifeSpeed;
      
    }

    public void Initialize(Vector2 KnifeDirection)
    {
        


        this.KnifeDirection = KnifeDirection;
        
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject,2f);
    }
    

    
}
