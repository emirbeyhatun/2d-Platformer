using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelScript : MonoBehaviour {
    [SerializeField]
    private GameObject Panel;
    private bool stopPlayer;
    private GameObject Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            bool[] array = SaveLoad.Load().savedLevels;
            




            array[SceneManager.GetActiveScene().buildIndex] = true;//making next scene true
            SaveLoad.Save(array,  ScoreAndCombo.Ornek.EndScore());

            
          
            Panel.SetActive(true);
            collision.gameObject.SetActive(false);
           

          
        }
    }

}
