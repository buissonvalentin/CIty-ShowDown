using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeBox : MonoBehaviour {

    public Button returnButton;
    public GameObject boxMainMenu;

    // Use this for initialization
    void Start () {
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

    void Return()
    {
        boxMainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
