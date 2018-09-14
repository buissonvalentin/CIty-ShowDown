using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakingBox : MonoBehaviour {

    public Button chercherPartie;
    public Button returnButton;
    public GameObject boxMainMenu;


    ServerManager serverManager;
    // Use this for initialization
    void Start () {

        serverManager = FindObjectOfType<ServerManager>();
        

        chercherPartie.onClick.AddListener(() =>
        {
            serverManager.JoinMatchMaking();
            transform.Find("LoadingImage").gameObject.SetActive(true);
        });

        returnButton.onClick.AddListener(() =>
        {
            Return();
        });
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Return();
        }
	}

    public void PlayerFound(string player)
    {
        transform.Find("Container").Find("Player2").Find("Pseudo").GetComponent<Text>().text = player;
        transform.Find("LoadingImage").gameObject.SetActive(false);
        transform.Find("ConnectedImage").gameObject.SetActive(true);
    }

    void Return()
    {
        transform.Find("Container").Find("Player2").Find("Pseudo").GetComponent<Text>().text = "???";
        transform.Find("LoadingImage").gameObject.SetActive(false);
        transform.Find("ConnectedImage").gameObject.SetActive(false);
        // leave matchmaking
        boxMainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
