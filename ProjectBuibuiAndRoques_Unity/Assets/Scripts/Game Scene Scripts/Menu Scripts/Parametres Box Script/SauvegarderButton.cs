using UnityEngine;
using UnityEngine.UI;
using ProjectRoquesAndBuiBui;
using Assets.Scripts.Others;


public class SauvegarderButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Ville v = FindObjectOfType<GameManager>().ville;
            //v.Pause();
            v.TempsRestant = FindObjectOfType<Timer>().time;
            SavesManager.SaveGame(v);
            //v.Play();
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
