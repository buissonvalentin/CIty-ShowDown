using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour {

    public Transform menu;

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() => {
            menu.gameObject.SetActive(false);
            if (menu.GetComponent<SelectionMenuNavigation>())
            {
                FindObjectOfType<Canvas>().GetComponent<Menu>().FermeMenuSelection();
            }
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
