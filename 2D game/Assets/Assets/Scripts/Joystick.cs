using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler { 
    
    public void OnPointerDown(PointerEventData veri)
    {
        if (gameObject.name == "SolBtn")
            Player.Ornek.goLeft = true;
        else if(gameObject.name == "SagBtn")
            Player.Ornek.goRight = true;

      


    }
    public void OnPointerUp(PointerEventData veri)
    {
      

        Player.Ornek.Stop();
        

    }
    
}
