using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {


    public Button nouvellePartie;
    public Button continuer;
    public Button charger;
    public Button parametres;
    public Button quitter;
    public Button partieSolo;
    public Button partieMulti;

    Transform boxGameMode;
    Transform boxMainMenu;
    Transform boxMatchMaking;
    Transform boxLoadSaves;

    // Use this for initialization
    void Start () {
        boxGameMode = transform.Find("BoxGameMode");
        boxMainMenu = transform.Find("BoxMainMenu");
        boxLoadSaves = transform.Find("BoxLoadSaves");

        nouvellePartie.onClick.AddListener(() =>
        {
            boxGameMode.gameObject.SetActive(true);
            boxMainMenu.gameObject.SetActive(false);
        });

        partieSolo.onClick.AddListener(() =>
        {
            FindObjectOfType<Manager>().StartGame();
        });

        partieMulti.onClick.AddListener(() =>
        {
            if (FindObjectOfType<ServerManager>().Connect())
            {
                boxGameMode.gameObject.SetActive(true);
                boxMatchMaking.gameObject.SetActive(true);
            }
            
        });

        charger.onClick.AddListener(() =>
        {
            boxLoadSaves.gameObject.SetActive(true);
            boxMainMenu.gameObject.SetActive(false); 
        });

        quitter.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
