using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Combat
{
    /// Courtesy of Akseli
    public class ButtonSideways : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool pressed;

        /// Method that causes pressed to be true when mouse is clicked
        public void OnPointerDown(PointerEventData eventData)
        {
            pressed = true;
        }

        /// Method that causes pressed to be false when mouse is released.
        public void OnPointerUp(PointerEventData eventData)
        {
            pressed = false;
        }

        /// Returns pressed value.
        public bool GetPressed
        {
            get
            {
                return pressed;
            }
        }
    }
}
