using ProjectRoquesAndBuiBui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxLois : MonoBehaviour {

    public Transform loiModel;

	// Use this for initialization
	void Start () {
        // Creation des box lois
        

        Ville v = FindObjectOfType<GameManager>().ville;

        foreach(Legislation l in v.LoisExistantes)
        {
            Transform temp = Instantiate(loiModel, transform.Find("Container"));
            temp.Find("Title").GetComponent<Text>().text = l.Nom;
            temp.Find("Description").GetComponent<Text>().text = l.Description;
            temp.gameObject.SetActive(true);
        }

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
