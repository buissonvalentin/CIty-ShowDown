﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsRotation : MonoBehaviour {

    public float speed = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Time.deltaTime * speed, 0, 0);
    }
}
