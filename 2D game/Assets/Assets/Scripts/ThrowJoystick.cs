using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowJoystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData veri)
    {
        
        if (gameObject.name == "ThrowBtn")
        {
            Player.Ornek.ThrowAnimation();

        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
      
    }
}
