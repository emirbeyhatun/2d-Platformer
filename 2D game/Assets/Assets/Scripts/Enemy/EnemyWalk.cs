using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour {
  
    [SerializeField]
    private float speed=8;
    [SerializeField]
    Vector2 yon;
    private float attackTimer;
    private float attackEnd = 1.75f;
    private bool attackEnable = true;
    [SerializeField]
    private Animator SwordColliderAnimator;



    [SerializeField]
    private float fightArea;
    [SerializeField]
    private float teleportArea;
    public GameObject Target { get; set; }
    private bool sagaBak;
    private static EnemyWalk ornek;
    [SerializeField]
    private bool isTeleportingEnabled;
    public static EnemyWalk Ornek
    {
        get
        {
            if (ornek == null)
            {
                ornek = GameObject.FindObjectOfType<EnemyWalk>();

            }
            return ornek;
        }
    }


    public bool FightArea
    {
        get
        {

            if (Target != null)
            {
                return Vector2.Distance(transform.parent.position, Target.transform.position) <= fightArea;
            }
            return false;
        }
    }

    public bool TeleportArea
    {
        get
        {

            if (Target != null)
            {
                return Vector2.Distance(transform.parent.position, Target.transform.position) >= teleportArea;
            }
            return false;
        }
    }
    private void Start()
    {
        sagaBak = false;

    }
    void Update()
    {
        isInRange();
        LookAtTarget();


    }
    void isInRange()
    {
        if (isTeleportingEnabled)
        {
            if (TeleportArea)
            {
                Teleport();
            } 
        }

        if (!FightArea)
            Move();
        else if (FightArea)
        {
            Hit();
        }

    }
    void Hit()
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
            SwordColliderAnimator.gameObject.SetActive(true);
           SwordColliderAnimator.SetTrigger("EnemyAttackTrigger");
        }

    }
    void Teleport()
    {
        Vector3 location = Target.transform.position;
        gameObject.transform.parent.transform.position = location + new Vector3(2, 0, 0);
      

    }
    private void Move()
    {

       
            transform.parent.Translate(yon * (speed * Time.deltaTime));
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Edge"))
        {

            
            ChangeDirection();


        }
        else if (collision.gameObject.CompareTag("AbsoluteEdge"))
        {
            Target = null;
            ChangeDirection();
        }
    }

    public void ChangeDirection()
    {

        yon = -yon;
        sagaBak = !sagaBak;

        transform.parent.localScale = new Vector3(transform.parent.localScale.x * -1, transform.parent.localScale.y, transform.parent.localScale.z);//turns it around



    }
    public Vector2 HandleDirection()
    {

        return sagaBak ? Vector2.right : Vector2.left;
    }
    public void LookAtTarget()
    {
        if (Target != null)
        {
            float xYon = Target.transform.position.x - transform.position.x;
           
            if (xYon < 0 && sagaBak/*if player is at the left and the character is looking right then turn character*/ || xYon > 0 && !sagaBak)
            {
                ChangeDirection();
            }
        }
    }
}
