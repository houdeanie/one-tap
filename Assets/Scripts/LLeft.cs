using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;


namespace Com.Houdini.OneTap
{
    public class LLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        static public bool lookLeft;
        public void OnPointerDown(PointerEventData eventData)
        {
            lookLeft = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            lookLeft = false;
        }



}

}

