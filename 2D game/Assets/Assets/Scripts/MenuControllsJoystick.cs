using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuControllsJoystick : MonoBehaviour, IPointerClickHandler
{
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(gameObject.name== "MainMenu")
        {
            SceneManager.LoadScene(0);
        }
        if (gameObject.name == "NextLevel")
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        if (gameObject.name == "RestartLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (gameObject.name == "Start")
        {
            SceneManager.LoadScene("LevelOne");
        }
        if (transform.parent.name == "Level1")
        {
            SceneManager.LoadScene("LevelOne");
        }
         if (transform.parent.name == "Level2")
        {
            SceneManager.LoadScene("LevelTwo");
        }
        if (transform.parent.name == "Level3")
        {
            SceneManager.LoadScene("LevelThree");
        }
        if (transform.parent.name == "Level4")
        {
            SceneManager.LoadScene("LevelBoss1");
        }
    }

    public void LevelSelector(string level)
    {
        SceneManager.LoadScene(level);
    }
}
