using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour {

    [SerializeField]
    private int lifePoints;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private GameObject hitEffect;
    [SerializeField]
    private GameObject spread;
    [SerializeField]
    private bool spreadTrue;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            lifePoints--;
            collision.gameObject.SetActive(false);
            StartCoroutine(HitEffect());
        }
        if (collision.gameObject.tag == "Sword")
        {
            lifePoints--;
            
            StartCoroutine(HitEffect());
        }
    }
    private void Update()
    {
        //if(lifePoints==2)
        //{
        //    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
        //}
        //else


          if (lifePoints == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else if (lifePoints <= 0)
        {

            if (spreadTrue)
            {
                SpreadAround(); 
            }
            lifePoints--;
            Destroy(gameObject);

        }
    }

    IEnumerator HitEffect()
    {
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitEffect.SetActive(false);

    }
    void SpreadAround()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject clone = Instantiate(spread, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 45)));
           
            clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300, 300), Random.Range(-300, 300)));
            Destroy(clone, 3f);
            
        }
      
    }
}
