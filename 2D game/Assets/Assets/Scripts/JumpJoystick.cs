using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpJoystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name == "JumpBtn")
        {
            DoubleJump.Ornek.jumpTouch = true;

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (gameObject.name == "JumpBtn")
        {
            DoubleJump.Ornek.jumpTouch = false;

        }
    }

  
}
