using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectRoquesAndBuiBui;


public class Timer : MonoBehaviour {
    float cpt = 0.0f;
    float interpolationPeriod = 1f;
    public int time = 1800;
    Ville v;
    
    // Use this for initialization
    void Start () {
        v = FindObjectOfType<GameManager>().ville;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(v == null)
        {
            v = FindObjectOfType<GameManager>().ville;
            return;
        }
        else
        {
            WriteTime();
        }
        if (!v.JeuEnPause)
        {
            cpt += Time.deltaTime;
        }
       

        if (cpt >= interpolationPeriod)
        {
            cpt = 0.0f;
            time--;
        }
        if(time == 0)
        {
            FindObjectOfType<GameManager>().FinDePartie();
        }
    }

    void WriteTime()
    {
        TimeSpan ts = new TimeSpan(0, 0, time);
        transform.Find("Text").GetComponent<Text>().text = ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds;
    }
}
