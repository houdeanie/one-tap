using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;


namespace Com.Houdini.OneTap
{
    public class LUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        static public bool lookUp;
        public void OnPointerDown(PointerEventData eventData)
        {
            lookUp = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            lookUp = false;
        }



}

}

