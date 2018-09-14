using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour {


    public GameObject[] planes;
    Vector3[] spawnPoints = { new Vector3(-1390, 0, 1570), new Vector3(2810, 0, 2336), new Vector3(440, 0, -1719) };

    int cpt;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        cpt++;
        if( cpt == 4 * 60 * 60)
        {
            cpt = 0;
            GeneratePlane();
        }
    }

    void GeneratePlane()
    {
        float y = Random.Range(100, 200);
        GameObject temp = planes[Random.Range(0, planes.Length)];
        Vector3 spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        spawn.y = y;

        GameObject plane = Instantiate(temp, spawn, Quaternion.identity);
        plane.transform.LookAt(new Vector3(600, y, 600));
        plane.transform.Rotate(new Vector3(0, Random.Range(-20, 20), 0)); 

        plane.GetComponent<Rigidbody>().velocity = 350 * plane.transform.forward;

        Destroy(plane, 30);
    }
}
