using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionneAmenagementBouton : MonoBehaviour {
    
    public MouseRayCast mouse;
    public GameObject amenagement;

    // Use this for initialization
    void Start () {
       Button btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            mouse.SetAmenagement(amenagement);
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
