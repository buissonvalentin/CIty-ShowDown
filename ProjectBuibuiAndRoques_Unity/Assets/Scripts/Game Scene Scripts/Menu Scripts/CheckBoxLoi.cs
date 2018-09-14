using ProjectRoquesAndBuiBui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheckBoxLoi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Sprite uncheckedSprite;
    public Sprite checkedSprite;
    bool isChecked;

    // Use this for initialization
    void Start () {
        isChecked = false;
        Ville v = FindObjectOfType<GameManager>().ville;
        
        transform.Find("CheckboxImage").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (isChecked)
            {
                v.DesactiverLegislation(transform.Find("Title").GetComponent<Text>().text);      
                isChecked = false;
                transform.Find("CheckboxImage").GetComponent<Image>().sprite = uncheckedSprite;
                
                
            }
            else
            {
                v.ActiverLegislation(transform.Find("Title").GetComponent<Text>().text);               
                isChecked = true;
                transform.Find("CheckboxImage").GetComponent<Image>().sprite = checkedSprite;
                
            }

        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        transform.Find("CheckboxImage").GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 0);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        transform.Find("CheckboxImage").GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 0);
    }


}
