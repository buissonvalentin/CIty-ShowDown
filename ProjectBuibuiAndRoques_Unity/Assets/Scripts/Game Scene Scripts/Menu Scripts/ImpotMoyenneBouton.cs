using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectRoquesAndBuiBui;

public class ImpotMoyenneBouton : MonoBehaviour {

    public float value;
    Ville v;

    // Use this for initialization
    void Start () {
        v = FindObjectOfType<GameManager>().ville;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            v.CoefImpotMoyenne += value;
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
