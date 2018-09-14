using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager : MonoBehaviour {

    public GameObject game;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
	}


    public void StartGame()
    {
        // cacher menu 
        // afficher game gameobject
        GameObject.Find("MainMenu").SetActive(false); 
        game.SetActive(true);
        
    }
}
