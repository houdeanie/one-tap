using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;


namespace Com.Houdini.OneTap
{
    public class MFor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        static public bool moveForward;
        public void OnPointerDown(PointerEventData eventData)
        {
            moveForward = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            moveForward = false;
        }



}

}

