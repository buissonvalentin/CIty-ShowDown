using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectRoquesAndBuiBui;

public class ProgressBarCulture : MonoBehaviour {

    Ville v;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (v != null)
        {
            transform.Find("ProgressBar").GetComponent<Image>().fillAmount = (float)(v.Culture / 100);
            transform.Find("Text").GetComponent<Text>().text = (float)v.Culture + " %";
        }
        else
        {
            v = FindObjectOfType<GameManager>().ville;
        }
    }
}
