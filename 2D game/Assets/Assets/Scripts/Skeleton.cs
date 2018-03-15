using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skeleton : MonoBehaviour {
    [SerializeField]
    protected int energy;
    [SerializeField]
    protected float speed;
    [SerializeField]
    private EdgeCollider2D swordCollider;
    public EdgeCollider2D SwordCollider
    {
        get { return swordCollider; }
    }
    [SerializeField]
    private List<string> strikeSources;
    public bool gotHit { get; set; }
    public abstract bool isDead{get;}
    [SerializeField]
    protected GameObject knifePrefab;
    [SerializeField]
    protected Transform knifeTransform;
    
    public Animator MyAnimator { get; private set; }
    public bool Attack { get; set; }
    protected bool lookRight;
    // Use this for initialization
    public virtual void Start () {

        MyAnimator = GetComponent<Animator>();
        lookRight = true;
    }

    public abstract IEnumerator Hit();
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(strikeSources.Contains(collision.tag))
        {
           
           if(LifeController.Ornek.LifePoints!=1)
            StartCoroutine(WhenHealthDecrease.Ornek.DisableColliderForATime());
            else
            {
                LifeController.Ornek.LifePoints--;
            }

        }
        
    }
   
    public void ChangeDirection()
    {
        

            lookRight = !lookRight;

        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);//we are assigning localscale -1 to turn it around
        


    }
    public void ChangeDirectionWhen(float Horizontal)
    {
        if (((Horizontal > 0 && !lookRight) || (Horizontal < 0 && lookRight)))
        {

            ChangeDirection();
        }


    }
    public /*virtual*/ void ThrowKnife(/*int value*/)
    {
       
            if (lookRight)
            {
                
                GameObject cloneKnife = Instantiate(knifePrefab, knifeTransform.position, Quaternion.Euler(new Vector3(0, 0, -90))) as GameObject;
                cloneKnife.GetComponent<Knife>().Initialize(Vector3.right);
            }
            else
            {
                GameObject cloneKnife = Instantiate(knifePrefab, knifeTransform.position, Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
                cloneKnife.GetComponent<Knife>().Initialize(Vector3.left);

            }
        
    }
    public void WhileAttackingEnableCollider()
    {
        
            swordCollider.enabled = true;
       
    }
    
}
