using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    [SerializeField]
    private GameObject keyActive;//key in canvas
    [SerializeField]
    private GameObject doorOpen;//door to open
   
    [SerializeField]
    private string level;//which scene we are in
    [SerializeField]
    private GameObject Panel;//panel to show
    [SerializeField]
    private bool useKey;//if we dont want to use key we should make our doorOpen active in the scene then if we reach the door level ends

    void OnTriggerEnter2D(Collider2D collision)
    {
       
            if (collision.gameObject.tag == "Player" && doorOpen.activeSelf)
            {

            bool[] array = SaveLoad.Load().savedLevels;
           
            array[SceneManager.GetActiveScene().buildIndex ] = true;//we are assigning next scene true
            int gold = SaveLoad.Load().gold;
             SaveLoad.Save(array,gold);
            Panel.SetActive(true);

        } 
        
        
            
    }

    void Update()
    {
        if (useKey)
        {
            if (keyActive.activeSelf)
            {
                doorOpen.SetActive(true);


            }
        }
      
    }

}
