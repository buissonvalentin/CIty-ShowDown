using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public delegate void EnterEvent();
    public delegate void LeaveEvent();

    public EnterEvent OnEnter;
    public LeaveEvent OnLeave;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnLeave();
    }
    
}
