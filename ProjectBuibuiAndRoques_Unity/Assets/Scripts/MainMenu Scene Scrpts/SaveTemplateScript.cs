using Assets.Scripts.Others;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTemplateScript : MonoBehaviour {

    public Save save;

    public Text dateSave;
    public Button loadButton;
    public Button deleteButton;
	// Use this for initialization

	void Start () {
        if(save != null)
            dateSave.text = String.Format("{0:d/M/yyyy HH:mm:ss}", save.SaveDate);

        loadButton.onClick.AddListener(() =>
        {
            SavesManager.LoadGame(save.Key);
        });

        deleteButton.onClick.AddListener(() =>
        {
            string titre = "Attention";
            string content = "Voulez vous vraiment supprimer cette sauvegarde ?";

            FindObjectOfType<MainMenu>().transform.Find("Prompt").GetComponent<PromptWindow>().OpenPrompt(DeleteSave, () => { }, titre, content);
        });
	}
	
    void DeleteSave()
    {
        SavesManager.DeleteSave(save.Key);
        FindObjectOfType<LoadSavesBox>().LoadSaves();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
