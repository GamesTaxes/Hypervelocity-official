using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// Courtesy of Akseli
public class ButtonSideways : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

    public bool GetPressed
    {
        get
        {
            return pressed;
        }
    }
}
