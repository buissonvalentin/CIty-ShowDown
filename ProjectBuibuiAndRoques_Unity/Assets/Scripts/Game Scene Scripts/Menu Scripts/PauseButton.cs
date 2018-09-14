using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectRoquesAndBuiBui;


public class PauseButton : MonoBehaviour {

    public Sprite spritePlay;
    public Sprite spritePause;
    bool jeuEnPause;
	// Use this for initialization
	void Start () {
        jeuEnPause = true;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (jeuEnPause)
            {
                FindObjectOfType<GameManager>().ville.Play();
                jeuEnPause = false;
                GetComponent<Image>().sprite = spritePause;
            }
            else
            {
                FindObjectOfType<GameManager>().ville.Pause();
                jeuEnPause = true;
                GetComponent<Image>().sprite = spritePlay;
            }
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
