using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackJoystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name == "AttackBtn")
        {

            Player.Ornek.AttackAnimation();

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      
    }
}
