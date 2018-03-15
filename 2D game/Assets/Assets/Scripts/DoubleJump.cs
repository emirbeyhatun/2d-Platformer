using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour {

    
   
    private bool airborn { get; set; }
    private bool canDoubleJump { get; set; }
    [SerializeField]
    private float jumpForce;
    private Rigidbody2D rb;
    private Animator myAnimator;
    //private float distToGround;
    private float horizontalDistToGround=0;
    [SerializeField]
    private float fallMultiplier = 2.5f;
    [SerializeField]
    private float lowJumpMultiplier = 2f;
    private static DoubleJump ornek;
    private bool isGrounded;
   
    public bool jumpTouch { get; set; }

    public static DoubleJump Ornek
    {
        get
        {
            if (ornek == null)
            {
                ornek = GameObject.FindObjectOfType<DoubleJump>();

            }
            return ornek;
        }
       
    }

   
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        airborn = false;
        
    }

    void WallRun()
    {
        if ((isLeftNextToGround() || isRightNextToGround()) && isGrounded)
        {

            myAnimator.SetFloat("characterSpeed", 0);

        }

    }

    public bool isRightNextToGround()
    {

        return Physics2D.Raycast(transform.position, Vector2.right, horizontalDistToGround + 0.1f, Player.Ornek.whichGround);

    }
    public bool isLeftNextToGround()
    {

        return Physics2D.Raycast(transform.position, Vector2.left, horizontalDistToGround - 0.1f, Player.Ornek.whichGround);

    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpTouch = true;
           
        }
        
       
        if (Player.Ornek.OntheGroundFunction() == true)
        {
            
            isGrounded = true;
            
            
            
                airborn = true;
                

            
        }
        else
        {
            isGrounded = false;
        }
       
        Kontrol();
       // WallRun();
       
    }

    void Kontrol()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;//jump improvement
        }else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (jumpTouch /*|| Input.GetKey(KeyCode.W)*/)
        {
            jumpTouch = false;
            myAnimator.SetTrigger("jumpAnimTrigger");
            if (  airborn )
            {
                
                rb.velocity = new Vector2(rb.velocity.x,0);//if we fall , because of velocity we cant jump  so make it 0 then we use force 
              
               
                rb.AddForce(new Vector2(0, jumpForce));
                canDoubleJump = true;

                airborn = false;


                jumpTouch = false;

            }
            else if (!isGrounded && canDoubleJump)
                {
               
                    airborn = false;
                    canDoubleJump = false;
                    myAnimator.SetTrigger("doubleJump");
                    myAnimator.SetBool("fall",false);

                   
                    rb.velocity = new Vector2(rb.velocity.x,0);
                    rb.AddForce(new Vector2(0, jumpForce*0.9f));
                }
           

        }

    }
}
