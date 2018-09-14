using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectRoquesAndBuiBui;

public class ImpotAiseBouton : MonoBehaviour {

    public float value;
    Ville v;

	// Use this for initialization
	void Start () {
        
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if(v != null)
            v.CoefImpotAisee += value;
        });
	}
	
	// Update is called once per frame
	void Update () {
		if(v == null)
        {
            v = FindObjectOfType<GameManager>().ville;
        }
	}
}
