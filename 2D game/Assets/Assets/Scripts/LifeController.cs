using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeController : MonoBehaviour {
    [SerializeField]
    private GameObject[] hearts;
    [SerializeField]
    private GameObject[] Blackhearts;
    [SerializeField]
    private int lifePoints;
    [SerializeField]
    private string level;
    [SerializeField]
    private GameObject DeathEffect;
    [SerializeField]
    private GameObject Player;
    public int LifePoints
    {
        get { return lifePoints; }
        set { lifePoints = value; }
    }
    private static LifeController Instance;


    public static LifeController Ornek
    {
        get
        {
            if (Instance == null)
            {
                Instance = GameObject.FindObjectOfType<LifeController>();

            }
            return Instance;
        }
    }
    private void Awake()
    {
        KeepOnlyOneInstance();
        
    }
    public void KeepOnlyOneInstance()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            
        }
    }
    private void Start()
    {


       
        
        lifePoints = 3;
    }

    private void Update()
    {
        CheckIfDead(level);
    }

    void CheckIfDead(string level)
    {
        if (lifePoints == 2)
        {
            hearts[2].SetActive(false);
            Blackhearts[2].SetActive(true);

        }
        else if (lifePoints == 1)
        {
            hearts[2].SetActive(false);
            hearts[1].SetActive(false);
            Blackhearts[1].SetActive(true);
            Blackhearts[2].SetActive(true);

        }
        



        if (lifePoints == 0)
        {
            lifePoints--;
            //place death animation, write that animation's behaviour script and place the  loadscene inside that
            hearts[0].SetActive(false);
            Blackhearts[0].SetActive(true);
            StartCoroutine( waitThenLoadLevel(level));
           



        }

    }
    IEnumerator waitThenLoadLevel(string level)//this is for death effects etc
    {
        Vector2 Position = Player.transform.position;
        //Player.layer = LayerMask.NameToLayer("Untouchable");
        //Destroy(Player.GetComponent<SpriteRenderer>());
        Player.SetActive(false);

        GameObject clone = Instantiate(DeathEffect, Position, Quaternion.identity);
      
        yield return new WaitForSeconds(2f);//we can use variable rather then numbers
        clone.SetActive(false);
        SceneManager.LoadScene(level);
    }
   public void CollectHearth(GameObject hearth) {

        if (lifePoints != 3)//if life is full , lifepoints wont increase
        {
            lifePoints++;
            hearth.SetActive(false);
        }

            if (hearts[1].activeSelf==false)//checks if second heart is notactive
            {
               
                hearts[1].SetActive(true);
                Blackhearts[1].SetActive(false);
            }
            else if (hearts[2].activeSelf == false)//checks if third heart is notactive
        {

                hearts[2].SetActive(true);
                Blackhearts[2].SetActive(false);
            }

           

        
    }
}
