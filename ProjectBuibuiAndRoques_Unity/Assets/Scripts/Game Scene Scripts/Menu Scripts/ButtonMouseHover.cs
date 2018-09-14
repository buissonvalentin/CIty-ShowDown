using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject ring;
    GameObject temp;

    void Start()
    {
       
    }

    private void Update()
    {
        
    }


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        temp = Instantiate(ring, transform.position, Quaternion.identity, transform); 
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        DestroyImmediate(temp);
    }
}
