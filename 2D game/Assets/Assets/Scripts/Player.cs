using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : Skeleton {

    
    public Rigidbody2D MyRigidbody { get; set; }
    
    
   [SerializeField]
    private Transform[] touchPoint;
    [SerializeField]
    private float touchRadius;
    [SerializeField]
    public LayerMask whichGround;
    [SerializeField]
    private bool skyControl;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private GameObject canvasKey;
    
    public bool OntheGround { get; set; }
    public bool Jump { get; set; }
    public Vector3 position { get; set; }
   private float time;
    [HideInInspector]
    public bool goLeft, goRight;

    
    public float characterSpeed = 8f, maxSpeed = 4f;



    

    private static Player ornek;
    
        
    public static Player Ornek
    {
        get
        {
            if (ornek == null)
            {
                ornek = GameObject.FindObjectOfType<Player>();
                
            }
            return ornek;
        }
    }

    public override bool isDead
    {
        get
        {
            return energy <= 0;
        }
    }
    private void Awake()
    {
        time = 0;
    }
    public override void Start () {
        base.Start();
       

        MyRigidbody = GetComponent<Rigidbody2D>();
       
        position = gameObject.transform.position;
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       
        if (!gotHit && !isDead)
        {
           
            OntheGround = OntheGroundFunction();

            BaseMovements();
            float yatay = Input.GetAxis("Horizontal");
            if (!base.Attack && (OntheGround || skyControl))
            {
                BaseMovementsKeyboard(yatay);
            }
            if (goLeft && !base.Attack && (OntheGround || skyControl))
            {
                SolaGit();


            }
            if(goRight && !base.Attack && (OntheGround || skyControl))
            {
                SagaGit();


            }
            ChangeDirectionWhen(MyRigidbody.velocity.x);


           
        }

    }
    void SagaGit()
    {

       
        float surat = 0.1f;

        


        MyRigidbody.velocity = new Vector2(surat * speed, MyRigidbody.velocity.y);

    }
    void SolaGit()
    {

        
        float surat =- 0.1f;

        


        MyRigidbody.velocity = new Vector2(surat * speed , MyRigidbody.velocity.y);



    }
    public void Stop()
    {
        goRight = false;
        goLeft = false;
        MyRigidbody.velocity = new Vector2(0, MyRigidbody.velocity.y);
    }
    void Update()
    {
       
        //if (!Darbede && !Oldumu)
        //{
            Controller();
            StartCoroutine(MovementLayers());
        //}

    }

    private void BaseMovementsKeyboard(float yatay)
    {
        #region HorizontalMovementController
        //if (MyRigidbody.velocity.y < 0)
        //{

        //    MyAnimator.SetBool("fall", true);

        //}
        //if (!base.Attack && (OntheGround || skyControl))
        //{
        //    MyRigidbody.velocity = new Vector2(yatay * speed * 1 / 6, MyRigidbody.velocity.y);


        //}
       

        //MyAnimator.SetFloat("characterSpeed", Mathf.Abs(yatay));
        #endregion   

        //if you dont delete this region your mobile button wont work war run animation, delete this region or make it comment line
        //if you want to use keyboard from your pc then make this region non comment but then run animation wont work with ui buttons

    }
    private void BaseMovements()
    {
        if (MyRigidbody.velocity.y < 0 )
        {
           
            MyAnimator.SetBool("fall", true);

        }

       
       
        MyAnimator.SetFloat("characterSpeed", Mathf.Abs(MyRigidbody.velocity.x));

    }

    
    private void Controller()
    {


        

        if (Input.GetKeyDown(KeyCode.F))
        {
            MyAnimator.SetTrigger("attackAnimTrigger");
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            MyAnimator.SetTrigger("jumpAnimTrigger");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (Time.time > time)
            {
                time = Time.time + 0.5f;
                MyAnimator.SetTrigger("throw");

                ThrowKnife();
            }

        }
    }
   
    public void AttackAnimation() 
    {
        MyAnimator.SetTrigger("attackAnimTrigger");
    }
    public void ThrowAnimation() {
        if (Time.time > time)
        {
            time = Time.time + 0.5f;
            MyAnimator.SetTrigger("throw");

            ThrowKnife();
        }
    }
    
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Coin")
        {
            collision.gameObject.SetActive(false);
            ScoreAndCombo.Ornek.Gold += 1;
            ScoreAndCombo.Ornek.SetGoldText();
        }

        if (collision.tag == "Hearth")
        {
            LifeController.Ornek.CollectHearth(collision.gameObject);//checks if life points full, if so then dont do anything to hearth but if it isn't then collect the hearth and set it not active
        }
        if (collision.tag == "Key")//we are activating key in canvas
        {
            collision.gameObject.SetActive(false);
            canvasKey.SetActive(true);

        }
    }

    

    
    
    public bool OntheGroundFunction()
    {
        if (MyRigidbody.velocity.y <= 0)
        {

            foreach(Transform point in touchPoint)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, touchRadius, whichGround);
                for(int i = 0; i < colliders.Length; i++)
                {

                    if (colliders[i].gameObject != gameObject)
                    {
                       
                        return true;
                    }
                }

            }

        }
        return false;
    }

    private IEnumerator MovementLayers()
    {
        if (!OntheGround )
        {
            yield return new WaitForSeconds(0.1f);
                MyAnimator.SetLayerWeight(1, 1);


        }else
        {
            yield return new WaitForSeconds(0.1f);
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    public override IEnumerator Hit()
    {
        energy -= 10;
        if (!isDead)
        {
            MyAnimator.SetTrigger("hit");
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
            MyAnimator.SetTrigger("death");
            
        }
        yield return null;
    }

    
}
