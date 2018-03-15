using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    private BossActionType eCurState;
    [SerializeField]
    Vector2 direction;
    [SerializeField]
    private GameObject Target;
    [SerializeField]
    private float fightArea;
    private Rigidbody2D parentRb;
    [SerializeField]
    private float speed;
    private bool lookRight;
    private float timerA;
    private int choose;
    private float jumpCooldown;
    private float attackTimer;
    private float attackEnd = 1.75f;
    private bool attackEnable = true;
    [SerializeField]
    private Animator SwordAnimator;
  
    public bool FightArea
    {
        get
        {

            if (Target != null)
            {
                return Vector2.Distance(transform.parent.position, Target.transform.position) <= fightArea;//if this is smaller then FightArea or equal then it returns dogru true.If not then returns false.
            }
            return false;//if target is null then this  returns false
        }
    }
    private bool ForOnce;
    private bool ForOnce2;
    public enum BossActionType
    {
        RangedAttacking,
        Moving,
        AvoidingObstacle,
        Patrolling,
        Attacking,
        JumpTowardsPlayer,
        JumpUp
    }


    private void Start()
    {
        ForOnce = true;
        timerA = 15f;
        choose = 0;
        
        jumpCooldown = 0f;
        lookRight = false;
        eCurState = BossActionType.Moving;
        parentRb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        TimerA();
        LookAtTarget();
        BossActions();

    }
   
    public void ChangeDirection()
    {

        direction = -direction;
        lookRight = !lookRight;

        transform.parent.localScale = new Vector3(transform.parent.localScale.x * -1, transform.parent.localScale.y, transform.parent.localScale.z);//turns boss around



    }
    public void LookAtTarget()
    {
        if (Target != null)
        {
            float xYon = Target.transform.position.x - transform.position.x;
            if(parentRb.transform.position.y+3< Target.transform.position.y && parentRb.transform.position.x <= Target.transform.position.x+ 1 && parentRb.transform.position.x >= Target.transform.position.x -1)//eger 3 birim ustundeysek 1 birim araliklarla saga sola gidip geliyo
            {
               
                return;
            }
           else  if (xYon <= 0 && lookRight || xYon > 0 && !lookRight)
            {
                ChangeDirection();
            }
        }
    }
    
    private void TimerA()
    {
        timerA -= Time.deltaTime;

        if (timerA <= 0 )
        {
            if (choose % 2 == 0)
            {
                timerA = 10f;
 
                Vector3 t = ManageDirectionForJump();
                parentRb.AddForce((-t +new Vector3(0,2,0))*20000);
                eCurState = BossActionType.RangedAttacking;
                choose = 1;
            }else

            if (choose % 2 == 1)
            {
                timerA = 10f;
 
                eCurState = BossActionType.Moving;
                choose = 0;
            }
        }


    }
    
    private void BossActions()
    {
        jumpCooldown -= 1 * Time.deltaTime;
        switch (eCurState)
        {
            case BossActionType.RangedAttacking:
                HandleRangedAttackState();
                break;

            case BossActionType.Moving:
                HandleMovingState();
                break;

            //case BossActionType.AvoidingObstacle:
            //    HandleAvoidingObstacleState();
            //    break;

            //case BossActionType.Patrolling:
            //    HandlePatrollingState();
            //    break;

            case BossActionType.Attacking:
                HandleAttackingState();
                break;
            case BossActionType.JumpTowardsPlayer:
               
                
                
                if (jumpCooldown<=0)
                HandleJumpTowardsPlayerState();


                eCurState = BossActionType.Moving;
                break;
        }
      

    }
    
    // Update is called once per frame
    

    private void HandleRangedAttackState()
    {
      
        if (ForOnce)
        {
            foreach (Transform child in transform.parent.gameObject.transform)
            {
                if (null == child)
                {
                    continue;
                }

                if (child.gameObject.GetComponent<EnemyThrow>() != null )
                {
                    ForOnce = false;
                    ForOnce2 = true;
                    child.gameObject.GetComponent<EnemyThrow>().Begin();
                }

            } 
        }

       

    }
    private void HandleMovingState()
    {

        if (ForOnce2)
        {
            foreach (Transform child in transform.parent.gameObject.transform)
            {
                if (null == child)
                {
                    continue;
                }

                if (child.gameObject.GetComponent<EnemyThrow>() != null)
                {
                    ForOnce2 = false;
                    ForOnce = true;
                    child.gameObject.GetComponent<EnemyThrow>().End();
                }
            }

        }
        if (FightArea)
        {
          
            eCurState = BossActionType.Attacking;
            return;
        }


        
       
       
            transform.parent.Translate(direction * (speed * Time.deltaTime));//move towards player


      



    }

     private void HandleJumpTowardsPlayerState()
    {
        jumpCooldown = 5;
        Vector3 t;


        t = ManageDirectionForJump();

        parentRb.AddForce(t * 30000);
       
        



    }
    //private void HandleAvoidingObstacleState()
    //{

    //}
    //private void HandlePatrollingState()
    //{

    //}
    private void HandleAttackingState()
    {

        if (!FightArea)
        {
            eCurState = BossActionType.Moving;
            return;
        }
        else
        {
            
                attackTimer += Time.deltaTime;

                if (attackTimer >= attackEnd)
                {
                    attackEnable = true;
                    attackTimer = 0;


                }
                if (attackEnable)
                {
                    attackEnable = false;
                    SwordAnimator.gameObject.SetActive(true);
                    SwordAnimator.SetTrigger("EnemyAttackTrigger");
                }

            


        }

        if (FightArea && Target.transform.position.y > transform.position.y + 3)
        {
            eCurState = BossActionType.JumpTowardsPlayer;
            return;


        }



    }

    private Vector3 ManageDirectionForJump()
    {
        Vector3 t;
        if (GameObject.Find("Player") != null)
        {
            float x = GameObject.Find("Player").transform.position.x - gameObject.transform.position.x;
            float y = GameObject.Find("Player").transform.position.y - gameObject.transform.position.y;
            float divX = x;
            float divY = y;
            if (Mathf.Abs(x) >= Mathf.Abs(y))
            {

                x /= Mathf.Abs(divX);

                y /= Mathf.Abs(divX);


            }
            else if (Mathf.Abs(y) > Mathf.Abs(x))
            {
                x = x / Mathf.Abs(divY);

                y = y / Mathf.Abs(divY);

            }
            t = new Vector3(x, y);
            return t;
        }
        else return Vector3.zero;
        
       
    }

}
