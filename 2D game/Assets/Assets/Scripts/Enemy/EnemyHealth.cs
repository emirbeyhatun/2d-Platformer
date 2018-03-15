using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    [SerializeField]
    private List<string> strikeSources;
    [SerializeField]
    private int lifePoints;
    [SerializeField]
    private GameObject bloodEffect;
    [SerializeField]
    private GameObject bloodEffect2;
    [SerializeField]
    private GameObject PopupText;
    [SerializeField]
    private GameObject Canvas;
    [SerializeField]
    private int comboGainOnHit;
    [SerializeField]
    private int comboGainOnKill;
   
    private GameObject Player;
    [SerializeField]
    private GameObject Panel;


    private bool isDead
    {
        get
        {
            return lifePoints <= 0;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (strikeSources.Contains(collision.tag))
        {
            StartCoroutine(Hit());
            
            if(collision.tag=="Knife")
            {
                WhenKnifeHitsFromSides(collision);
                
              
               
            }

            
        }
        if (collision.tag == "FallingKnife")
        {
            StartCoroutine(Hit());
            WhenKnifeFallsUponEnemy( collision);


        }
    }
    private void WhenKnifeFallsUponEnemy(Collider2D collision)
    {//This code is for making Knife Stuck on enemy to give stuck effect like when an arrow hits the tree arrow stucks on tree
        
        if (collision.GetComponent<HingeJoint2D>() != null)
        {
            Destroy(collision.GetComponent<HingeJoint2D>()); //for performance ,this destroy functions must not be used , we must use object pooling system
        }
        if (collision.GetComponent<Knife>() != null)
        {
            collision.GetComponent<Knife>().enabled = false;
        }
        Destroy(collision.GetComponent<Rigidbody2D>());

        collision.transform.parent = transform.parent;  
        collision.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y - 1, 0);
        collision.GetComponent<Collider2D>().enabled = false;
        collision.GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
    private void WhenKnifeHitsFromSides(Collider2D collision)
    {
        //This code is for making Knife Stuck on enemy to give stuck effect like when an arrow hits the tree arrow stucks on tree
        if (collision.GetComponent<HingeJoint2D>() != null)
        {
            Destroy(collision.GetComponent<HingeJoint2D>());
        }
        if (collision.GetComponent<Knife>() != null)
        {
            Destroy(collision.GetComponent<Knife>()); //for performance ,this destroy functions must not be used , we must use object pooling system
        }

        Destroy(collision.GetComponent<Rigidbody2D>());


        collision.transform.parent = transform.parent;
        collision.GetComponent<Collider2D>().enabled = false;
        collision.GetComponent<SpriteRenderer>().sortingOrder = 0;

        if (GameObject.Find("Player") != null)
        {
            float t = gameObject.transform.position.x - GameObject.Find("Player").transform.position.x;
            if (t > 0)
                collision.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-110f, -70f)));
            else if (t < 0)
                collision.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(110f, 70f)));
        }
    }
    private void Update()
    {
        if (isDead)
        {
            
            DestroyEnemy();
            //transform.parent.gameObject.SetActive(false);
            //Stop movements
            //Death effects and Combo  func
        }
    }
    private IEnumerator Hit()
    {
        lifePoints -= 1;
        
        BloodEffect2();


        PopupTextFunction(comboGainOnHit);//this is for giving hit effects like, numbers  10 20 50 pup on character and fastly fades
        if(isDead)
        {
            if (gameObject.transform.parent.name == "Boss")
            {
                bool[] array = new bool[] { true, true, true, true };
                SaveLoad.Save(array,ScoreAndCombo.Ornek.EndScore());

                Player.SetActive(false);
                Panel.SetActive(true);
            }

            PopupTextFunction(comboGainOnKill);

                BloodEffect();
            
            //MyAnimator.SetLayerWeight(1, 0);
            //MyAnimator.SetTrigger("death");

        }
        yield return null;
    }
    void BloodEffect2()
    {
        GameObject clone2 = Instantiate(bloodEffect2, transform.parent.position, Quaternion.identity);

        ScoreAndCombo.Ornek.Combo += comboGainOnHit;
        ScoreAndCombo.Ornek.SetComboText();//this section is for saving game score with data persistance 
        Destroy(clone2, 2f);
    }
    void BloodEffect()
    {
       
        GameObject clone = Instantiate(bloodEffect, transform.parent.position, Quaternion.identity);//if we dont use clones it would destroy the  prefab instead
        ScoreAndCombo.Ornek.Combo += comboGainOnKill;//Display combo numbers
        ScoreAndCombo.Ornek.SetComboText();//this section is for saving game score with data persistance 
        Destroy(clone, 2f);
       
    }
    void DestroyEnemy()
    {
        transform.parent.gameObject.layer = LayerMask.NameToLayer("Untouchable");

        foreach (Transform child in transform.parent.gameObject.transform)
        {
            if (null == child)                                              //changing it's and it's childer's layers and wait 2 secs then destroy.Because inside WhenHealthDecrease  IE numerator is runing thus it should stay active for 2 secs.  if it is detroyed immediately , IEnumerator's part after wait  wont run
            {
                continue;
            }
            child.gameObject.layer = LayerMask.NameToLayer("Untouchable");
            if (child.gameObject.GetComponent<EnemyThrow>() != null)
            {
                
                child.gameObject.GetComponent<EnemyThrow>().End();
            }

            if (child.GetComponent<SpriteRenderer>()!=null)
                child.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (transform.parent.GetComponent<SpriteRenderer>() != null)
            transform.parent.GetComponent<SpriteRenderer>().enabled = false;
        
        Destroy(transform.parent.gameObject, 2f);
    }

    void PopupTextFunction(int hitPoint)
    {
        Vector2 randomizePosition = new Vector2(transform.parent.position.x + Random.Range(-.5f, .5f), transform.parent.position.y + Random.Range(-.5f, .5f));
        GameObject textClone = Instantiate(PopupText, randomizePosition, Quaternion.identity);
        
        textClone.transform.SetParent(Canvas.transform);
        textClone.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);


        textClone.GetComponentInChildren<Text>().text = hitPoint.ToString();
        Destroy(textClone, 1f);
    }
}
