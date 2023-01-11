using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.EventSystems;

 public class Onmouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
 {
    public GameObject logoText;

     public void OnPointerEnter(PointerEventData eventData)
     {
        logoText.SetActive(true);
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
        logoText.SetActive(false);
     }
 }