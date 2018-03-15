using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : MonoBehaviour {

    [SerializeField]
    private GameObject Axe;

    [Range(-1,1)]
    [SerializeField]
    private float Horizontal;
    [Range(-1, 1)]
    [SerializeField]
    private float Vertical;
    [SerializeField]
    private float throwGap;
    [SerializeField]
    private bool playerTarget;

    






    //private bool throwAxe;
    //private bool throwOnce;
    
    private Vector3 direction;

    //private void FixedUpdate()
    //{
    //    if (throwAxe)
    //        Timer();
    //    else if(!throwAxe)
    //        StopTimer();
    //}
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
            playerTarget = false;
    }

    void Timer()
    {
        //if (throwOnce)
        //{
            InvokeRepeating("Throw", 0, throwGap);
            //throwOnce = false;
        //}

    }
    void StopTimer()
    {
        
        CancelInvoke();
        //throwOnce = true;

    }
    private void OnBecameVisible()
    {
        Timer();
    }
  
    private void OnBecameInvisible()
    {
        StopTimer();

    }

    public void Begin()
    {
        Timer();
    }
    public void End()
    {
        StopTimer();
    }


private void Start()
    {
        //throwOnce = true;
        
    }

    void Throw()
    {
        if (playerTarget)
        {
            FindPlayer();

        }
        else if (!playerTarget)
        {
            direction.x = Horizontal;
            direction.y = Vertical;

        }


        GameObject cloneKnife = Instantiate(Axe, transform.parent.position, Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
        
        cloneKnife.GetComponent<Knife>().Initialize(direction);
    }
    void FindPlayer()
    {
        Vector3 t;
        
        float x = GameObject.Find("Player").transform.position.x - gameObject.transform.parent.position.x;
      

        float y = GameObject.Find("Player").transform.position.y - gameObject.transform.parent.position.y;

        float divX = x;
        float divY = y;
        if(Mathf.Abs(x) >= Mathf.Abs(y))
        {
            
            x/= Mathf.Abs(divX);
           
            y /=  Mathf.Abs(divX);
            
            
        }
        else if(Mathf.Abs(y) > Mathf.Abs(x))
        {
            x = x / Mathf.Abs(divY);
            
            y = y / Mathf.Abs(divY);

        }

      
        t = new Vector3(x, y);






        direction = t;


    }
}
