using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfficheMenuInfo : MonoBehaviour {

    public Transform menuInfo;

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (menuInfo.gameObject.activeInHierarchy)
            {
                menuInfo.gameObject.SetActive(false);
            }
            else
            {
                menuInfo.gameObject.SetActive(true);
            }
            
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
