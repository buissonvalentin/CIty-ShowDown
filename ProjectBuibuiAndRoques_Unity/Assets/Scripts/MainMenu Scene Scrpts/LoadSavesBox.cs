using Assets.Scripts.Others;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSavesBox : MonoBehaviour {

    public Transform saveTemplate;
    public Button returnButton;
    public GameObject boxMainMenu;

    // Use this for initialization


    // Use this for initialization
    void Start()
    {
        returnButton.onClick.AddListener(() =>
        {
            Return();
        });

        LoadSaves();
    }

    // Update is called once per frame
    void Update()
    {
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

    public void LoadSaves()
    {
        List<Save> saves = SavesManager.FetchSaves();
        Transform container = transform.Find("MyScrollView").Find("Container");

        container.DetachChildren();

        foreach (Save s in saves)
        {
            Transform t = Instantiate(saveTemplate);
            t.parent = container;
            t.GetComponent<SaveTemplateScript>().save = s;
            t.gameObject.SetActive(true);
        }
        
    }
    
}
