using ProjectRoquesAndBuiBui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BonusActivationButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite activatedSprite;
    bool activated;

	// Use this for initialization
	void Start () {
        activated = false;
        Ville v = FindObjectOfType<GameManager>().ville;

        transform.Find("ActivateImage").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!activated)
            {
                Debug.Log("click");
                if (v.ActiverBonus(transform.Find("Title").GetComponent<Text>().text))
                {
                    activated = true;
                    transform.Find("ActivateImage").GetComponent<Image>().sprite = activatedSprite;
                    
                }

            }
        });
    }



    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        transform.Find("ActivateImage").GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 0);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        transform.Find("ActivateImage").GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 0);
    }
}
