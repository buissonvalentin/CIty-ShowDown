using ProjectRoquesAndBuiBui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGenerator : MonoBehaviour {

    int cpt = 0;
    public bool canGenerate;
    public List<GameObject> cars;
    Transform spawn;

    int countExit;
    int countRoad;
    Ville v;

	// Use this for initialization
	void Start () {
        countExit = 0;
        countRoad = 0;
        v = FindObjectOfType<GameManager>().ville;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        return;
        
        if(countRoad >= 10)
        {
            cpt++;
        }
        

        if(cpt >= 90)
        {
            cpt = 0;
            GeSpawnTransform();
            GameObject carTemp = Instantiate(cars[Random.Range(0,cars.Count)], spawn.position , Quaternion.Euler((spawn.rotation.eulerAngles - Quaternion.Euler(0f,90f,0f).eulerAngles)));
            Destroy(spawn.gameObject);
            spawn = null;
        }
        CountRoadStats();
	}

    void GeSpawnTransform()
    {
        List<GameObject> listRoutes = new List<GameObject>();

        foreach (GameObject ga in FindObjectsOfType<GameObject>())
        {
            if (ga.GetComponent<RouteOrientationManager>() && ga.GetComponent<RouteOrientationManager>().NombresDeSorties() == 1 && !(ga.GetComponent<AmenagementPrefab>().Amenagement as Route).EstSortie)
            {
                listRoutes.Add(ga);
            }
        }

        spawn = Instantiate(listRoutes[Random.Range(0, listRoutes.Count)].transform);
        spawn.gameObject.SetActive(false);

        if ((int)spawn.rotation.eulerAngles.y == 90)
        {
            spawn.position += new Vector3(2f, 0, 0);
        }
        else if ((int)spawn.rotation.eulerAngles.y == 180)
        {
            spawn.position += new Vector3(0, 0, -2f);
        }
        else if ((int)spawn.rotation.eulerAngles.y == 270 || (int)spawn.rotation.eulerAngles.y == -90)
        {
            spawn.position += new Vector3(-2f, 0, 0);
        }
        else
        {
            spawn.position += new Vector3(0, 0, 2f);
        }
    }

    void CountRoadStats()
    {
        countRoad = 0;
        foreach (Amenagement a in v.Amenagements)
        {
            if(a is Route)
            {
                countRoad++;
            }
        }
    }
}
