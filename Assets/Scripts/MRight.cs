using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;


namespace Com.Houdini.OneTap
{
    public class MRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        static public bool moveRight;
        public void OnPointerDown(PointerEventData eventData)
        {
            moveRight = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            moveRight = false;
        }



}

}

