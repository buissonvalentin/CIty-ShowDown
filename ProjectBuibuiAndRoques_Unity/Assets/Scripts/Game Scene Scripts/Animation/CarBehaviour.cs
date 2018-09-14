using ProjectRoquesAndBuiBui;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CarBehaviour : MonoBehaviour {

    Transform dest;
    Transform inter;
    Vector3 interDest;
    NavMeshAgent agent;
    string pos;
    float rot;

	// Use this for initialization
	void Start () {
        
        dest = GetDestinationTransform();

        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(dest.position);
        inter = null;
    }
	
	// Update is called once per frame
	void Update () {

        if(inter == null)
        {
            CheckIsOnIntersection();
        }

        if(interDest != Vector3.zero)
        {
            agent.SetDestination(interDest);
        }
        else
        {
            agent.SetDestination(dest.position);
        }
        

        if (agent.remainingDistance == 0)
        {
            if(interDest != Vector3.zero)
            {
                inter = null;
                interDest = Vector3.zero;
                pos = "";
                rot = 0;
                agent.SetDestination(dest.position);
            }
            else
            {
                Destroy(gameObject);
            }
           
        }

        if (inter != null && interDest == Vector3.zero)
        {
            Debug.Log("rot : " + rot);
            if(pos == "t") // 90
            {
                if(rot > 90) // going r;
                {
                    interDest = inter.position + new Vector3(-1.5f, 0, -7f);
                }
                if(rot < 89)
                {
                    interDest = inter.position + new Vector3(1.5f, 0, 7f);
                }
            }
            if (pos == "r") // 180
            {
                if (rot > 180) // going r;
                {
                    interDest = inter.position + new Vector3(-7f, 0, 1.5f);
                }
                if(rot < 179)
                {
                    interDest = inter.position + new Vector3(7f, 0, -1.5f);
                }
                
            }
            if (pos == "b") // -90
            {
                if(rot < -90) // going r;
                {
                    interDest = inter.position + new Vector3(1.5f, 0, 7f);
                }
                if ((rot > -91 && rot < - 60) || (rot > 260 && rot < 269)) 
                {
                    interDest = inter.position + new Vector3(-1.5f, 0, -7f);
                }
            }
            if (pos == "l") // 0
            {
                if(rot > 0) // going r;
                {
                    interDest = inter.position + new Vector3(7f, 0, -1.5f);
                }
                if (rot < -1 || rot > 344) 
                {
                    interDest = inter.position + new Vector3(-7f, 0, 1.5f);
                }
            }
        }

    }

    Transform GetDestinationTransform()
    {
        int layerMask = LayerMask.GetMask("Route");
        List<GameObject> listRoutes = new List<GameObject>();

        foreach (GameObject ga in FindObjectsOfType<GameObject>())
        {
            if (ga.GetComponent<RouteOrientationManager>() && ga.GetComponent<RouteOrientationManager>().NombresDeSorties() == 1 && !(ga.GetComponent<AmenagementPrefab>().Amenagement as Route).EstSortie)
            {
                listRoutes.Add(ga);
            }
        }

        int index = Random.Range(0, listRoutes.Count);
        Transform temp =  Instantiate(listRoutes[index].transform);

        while((Mathf.Min(Mathf.FloorToInt(temp.transform.position.x) / 10)) == (Mathf.Min(Mathf.FloorToInt(transform.position.x) / 10)) && (Mathf.Min(Mathf.FloorToInt(temp.transform.position.z) / 10)) == (Mathf.Min(Mathf.FloorToInt(transform.position.z) / 10)))
        {
            listRoutes.RemoveAt(index);
            DestroyImmediate(temp.gameObject);
            temp = null;
            temp = Instantiate(listRoutes[Random.Range(0, listRoutes.Count)].transform);
        }

        temp.gameObject.SetActive(false);

        if ((int)temp.rotation.eulerAngles.y == 90)
        {
            temp.position += new Vector3(-2f, 0, 0);
        }
        else if ((int)temp.rotation.eulerAngles.y == 180)
        {
            temp.position += new Vector3(0, 0, 2f);
        }
        else if ((int)temp.rotation.eulerAngles.y == 270 || (int)temp.rotation.eulerAngles.y == -90)
        {
            temp.position += new Vector3(2f, 0, 0);
        }
        else
        {
            temp.position += new Vector3(0, 0, -2f);
        }
        return temp;
        
        
    }

    void CheckIsOnIntersection()
    {
        int x = Mathf.FloorToInt(transform.position.x) / 10;
        int z = Mathf.FloorToInt(transform.position.z) / 10;

        
        List<GameObject> listRoutes = new List<GameObject>();

        foreach (GameObject ga in FindObjectsOfType<GameObject>())
        {
            if (ga.GetComponent<RouteOrientationManager>() && (Mathf.FloorToInt(ga.transform.position.x) / 10) == x && (Mathf.FloorToInt(ga.transform.position.z) / 10) == z && ga.GetComponent<RouteOrientationManager>().NombresDeSorties() > 2)
            {
                inter = ga.transform;
                rot = transform.rotation.eulerAngles.y;
                if ((transform.rotation.eulerAngles.y < 15 && transform.rotation.eulerAngles.y > -15) || (transform.rotation.eulerAngles.y < 375 && transform.rotation.eulerAngles.y > 345))
                {
                    pos = "l";
                }
                if (transform.rotation.eulerAngles.y < 195 && transform.rotation.eulerAngles.y > 165)
                {
                    pos = "r";
                }
                if (transform.rotation.eulerAngles.y < 105 && transform.rotation.eulerAngles.y > 75)
                {
                    pos = "t";
                }
                if ((transform.rotation.eulerAngles.y > -105 && transform.rotation.eulerAngles.y < -75) || (transform.rotation.eulerAngles.y > 255 && transform.rotation.eulerAngles.y < 285))
                {
                    pos = "b";
                }
                return;
            }
        }
    }
}
