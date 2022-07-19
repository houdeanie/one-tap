using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;


namespace Com.Houdini.OneTap
{
    public class LDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        static public bool lookDown;
        public void OnPointerDown(PointerEventData eventData)
        {
            lookDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            lookDown = false;
        }



}

}

