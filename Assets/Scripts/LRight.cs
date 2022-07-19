using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;


namespace Com.Houdini.OneTap
{
    public class LRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        static public bool lookRight;
        public void OnPointerDown(PointerEventData eventData)
        {
            lookRight = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            lookRight = false;
        }



}

}

