using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsRotation : MonoBehaviour {

    // Use this for initialization
    float speed;
	void Start () {
        speed = Random.Range(20f, 60f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, Time.deltaTime * speed);
    }
}
