using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;


namespace Com.Houdini.OneTap
{
    public class MBack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        static public bool moveBackwards;
        public void OnPointerDown(PointerEventData eventData)
        {
            moveBackwards = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            moveBackwards = false;
        }



}

}

