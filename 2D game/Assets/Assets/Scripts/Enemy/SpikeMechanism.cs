using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMechanism : MonoBehaviour {


    [SerializeField]
    private float amount;
    private float timer;
    private Vector3 firstPosition;
    private bool CheckUp;
    [SerializeField]
    private int downDistance;
    [SerializeField]
    private float FirstUpWaitTime;
    [SerializeField]
    private float downWaitTime;
    [SerializeField]
    private float upWaitTime;
    [SerializeField]
    private bool X;

    private void Start()
    {
        timer = FirstUpWaitTime;
        CheckUp = true;
        firstPosition = transform.position;
    }
    private void Update()
    {

        Timer();
    }
    private void Timer()
    {
        timer -= Time.deltaTime;
        
        if (timer<=0)
        {
            if (CheckUp)
            {
                Up();
            }
            else if (!CheckUp)
            {
                Down();
            } 
        }
    }
    private void Down()
    {
        if (!X)
        {
            if (firstPosition.y - downDistance < transform.position.y)
            {
                gameObject.transform.position += new Vector3(0, amount, 0);
            }
            else if (firstPosition.y - downDistance >= transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, firstPosition.y - downDistance, 0);
                gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
                amount = -amount;
                CheckUp = true;
                timer = downWaitTime;
            }
        }
        else if (X)
        {
            if (firstPosition.x - downDistance < transform.position.x)
            {
                gameObject.transform.position += new Vector3(amount, 0, 0);
            }
            else if (firstPosition.x - downDistance >= transform.position.x)
            {
                transform.position = new Vector3(firstPosition.x, transform.position.y, 0);
                amount = -amount;
                CheckUp = false;
                timer = upWaitTime;
            }
        }
    }
    private void Up()
    {
        if (!X)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            if (firstPosition.y > transform.position.y)
            {
                gameObject.transform.position += new Vector3(0, amount, 0);
            }
            else if (firstPosition.y <= transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, firstPosition.y, 0);
                amount = -amount;
                CheckUp = false;
                timer = upWaitTime;
            } 
        }
        else if (X)
        {
            if (firstPosition.x > transform.position.x)
            {
                gameObject.transform.position += new Vector3(amount, 0, 0);
            }
            else if (firstPosition.x <= transform.position.x)
            {
                transform.position = new Vector3( firstPosition.x, transform.position.y, 0);
                amount = -amount;
                CheckUp = false;
                timer = upWaitTime;
            }
        }

    }

}
