using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AfficheMenuSelectionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Menu menu;
    GameObject temp;
    public GameObject ring;
    public Transform menuSelection;
    bool isOpen;

    // Use this for initialization
    void Start () {
        isOpen = false;
        menu = GameObject.Find("Canvas Menu").GetComponent<Menu>();
        Button b = GetComponent<Button>();
        b.onClick.AddListener(() =>
        {
            if (!isOpen)
            {
                menu.AfficheMenuSelection(menuSelection, transform);
                isOpen = true;
            }
            else
            {
                menu.FermeMenuSelection();
                isOpen = false;
                Destroy(temp);
                temp = null;
            }
        });
        
	}
	
	// Update is called once per frame
	void Update () {
        if (isOpen && temp == null)
        {
            temp = Instantiate(ring, transform.position, Quaternion.identity, transform);
        }
	}

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(temp == null)
            temp = Instantiate(ring, transform.position, Quaternion.identity, transform);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!isOpen)
        {
            Close();
        }
    }

    public void Close()
    {
        isOpen = false;
        Destroy(temp);
        temp = null;
    }
}
