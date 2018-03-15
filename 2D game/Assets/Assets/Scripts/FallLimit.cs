using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallLimit : MonoBehaviour {

    [SerializeField]
    private float limitY;
    [SerializeField]
    private bool resetLevel;
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (!resetLevel)
                PlayerFall();
            else if (resetLevel)
                ResetLevel();
        }
        EnemyFall();

    }
    private void PlayerFall()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.y < limitY)
        {
            if (LifeController.Ornek.LifePoints <= 1)
            {
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.buildIndex);
            }
            else
            {

                LifeController.Ornek.LifePoints--;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = Player.Ornek.position;

            }


        }
    }
    private void ResetLevel()
    {
        if( GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.y < limitY )
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void EnemyFall()
    {
        //if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>().position.y < limitY)
        //{

            //GameObject.FindGameObjectWithTag("Enemy").gameObject.SetActive(false);

        //we need collider, trigger because there are lots of Enemy with Enemy tag





        //}
    }
}
