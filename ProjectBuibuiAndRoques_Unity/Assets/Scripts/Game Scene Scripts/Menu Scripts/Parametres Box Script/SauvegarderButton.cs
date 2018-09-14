using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Web.Script.Serialization;
using UnityEngine.UI;
using ProjectRoquesAndBuiBui;
using System.IO;
using System;
using System.Text;

public class SauvegarderButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("saving ...");
            Ville v = FindObjectOfType<GameManager>().ville;
            v.Pause();
            v.TempsRestant = FindObjectOfType<Timer>().time;
            string jsonObj = new JavaScriptSerializer().Serialize(FindObjectOfType<GameManager>().ville);
            string key = GetKey();


            StreamWriter sw = new StreamWriter("Saves\\" + key + ".txt");
            sw.Write(jsonObj);
            sw.Close();


            sw = new StreamWriter("Saves\\Saves.txt", true);
            sw.WriteLine(key + "\\" + DateTime.Now);
            sw.Close();

            v.Play();
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    string GetKey()
    {
        //32 - 125 sans 46
        string key = "";
        for(int i = 0; i < 15; i++)
        {
           
            int rdm;
            do
            {
                rdm = UnityEngine.Random.Range(32, 125);
            }
            while (rdm == 46 || rdm == 92);
            key += (char)rdm;
        }

        return key;
    }
}
