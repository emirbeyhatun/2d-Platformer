using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

    //[SerializeField]
    //private float dashForce;
    //float _doubleTapTimeD;
    //float _doubleTapTimeA;
   private Rigidbody2D MyRigidbody;
  
    [SerializeField]
    private float dashPower;
    private void Start()
    {
        MyRigidbody = gameObject.GetComponent<Rigidbody2D>();

    }
    private float _doubleTapTimeD;
    private float _doubleTapTimeA;
   
    public DashState dashState;
    public float dashTimer;
    public float maxDash = 20f;
    
    public Vector2 savedVelocity;

    void Update()
    {
        DashMove();


    }
    void DashMove()
    {
       




        switch (dashState)
        {
            case DashState.Ready:
                if (Input.GetKeyDown(KeyCode.D))
                {

                    if (Time.time < _doubleTapTimeD + .3f)//it should press 2 times in 0.3secs
                    {

                       
                        savedVelocity.x = MyRigidbody.velocity.x;

                        dashState = DashState.Dashing;

                    }
                    _doubleTapTimeD = Time.time;
                   
                }else
                if (Input.GetKeyDown(KeyCode.A))
                {

                    if (Time.time < _doubleTapTimeA + .3f)//it should press 2 times in 0.3secs
                    {

                       
                        savedVelocity.x = MyRigidbody.velocity.x;

                        dashState = DashState.Dashing;

                    }
                    _doubleTapTimeA = Time.time;
                }

                break;
            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;
                MyRigidbody.AddForce(new Vector3(Vector3.Normalize(MyRigidbody.velocity).x * dashPower, 0, 0));
                if (dashTimer >= maxDash)
                {
                    dashTimer = maxDash;
                    MyRigidbody.velocity = new Vector2(savedVelocity.x, MyRigidbody.velocity.y);
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
               
                dashTimer -= Time.deltaTime*3;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }

public enum DashState
{
    Ready,
    Dashing,
    Cooldown
}
}
