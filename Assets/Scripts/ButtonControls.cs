using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;


namespace Com.Houdini.OneTap
{
    public class ButtonControls : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        static public bool buttonControls;
        public void OnPointerDown(PointerEventData eventData)
        {
            buttonControls = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            buttonControls = false;
        }



    }

}

