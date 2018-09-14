using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRotation : MonoBehaviour {

    public float speed = 60;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        (transform as RectTransform).Rotate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
	}
}
