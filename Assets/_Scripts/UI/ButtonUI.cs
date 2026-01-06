using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace _Scripts.UI
{
    public class ButtonUI : MonoBehaviour, IPointerDownHandler
    {
        public UnityEvent onClick =  new UnityEvent();

        public void OnPointerDown(PointerEventData eventData)
        {
            onClick.Invoke();
        }
    }
}