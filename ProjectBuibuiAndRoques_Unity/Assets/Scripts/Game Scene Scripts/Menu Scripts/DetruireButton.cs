using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetruireButton : MonoBehaviour {

    public MouseRayCast mouse;
	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            mouse.ToggleDestroyMode();
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
