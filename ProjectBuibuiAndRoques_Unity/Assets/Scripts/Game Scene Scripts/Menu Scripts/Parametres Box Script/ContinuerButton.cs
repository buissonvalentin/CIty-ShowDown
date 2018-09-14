using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinuerButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameObject.Find("BoxParametre").SetActive(false);
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
