using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;


namespace Com.Houdini.OneTap
{
    public class MLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        static public bool moveLeft;
        public void OnPointerDown(PointerEventData eventData)
        {
            moveLeft = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            moveLeft = false;
        }



}

}

