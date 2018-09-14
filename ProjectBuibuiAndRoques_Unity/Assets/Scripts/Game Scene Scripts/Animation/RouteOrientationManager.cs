using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectRoquesAndBuiBui;

public class RouteOrientationManager : MonoBehaviour {

    public GameObject routeDroite;
    public GameObject routeAngle;
    public GameObject route3sortie;
    public GameObject routeImpasse;
    public GameObject route4sorties;

    string routeDroiteNav = "2wayStraight";
    string routeAngleNav = "2wayAngle";
    string route3sortiesNav = "3way";
    string routeImpasseNav = "1way";
    string route4sortiesNav = "4way";

    bool top = false;
    bool left = false;
    bool bot = false;
    bool right = false;

    int x = -1;
    int y = -1;

    Ville ville;
    Amenagement a;


    // Use this for initialization
    void Start () {
        ville = FindObjectOfType<GameManager>().ville;
        a = GetComponent<AmenagementPrefab>().Amenagement;
        if ((a as Route).EstSortie)
        {
            a.PosY = 41;
            a.PosX = 0;
            ville.PlacerUnAmenagement(a);
        }
    }

    private void Update()
    {
        
        UpdateOrientation();
    }

    public void UpdateOrientation()
    {


        DesactivateAllChildren();
        transform.Rotate(new Vector3(0, 0, 0));
        transform.rotation = Quaternion.identity;
        top = false;
        left = false;
        bot = false;
        right = false;

        int sorties = NombresDeSorties();

        if (sorties == 0)
        {
            SwapWith(routeImpasse);
            ActivateNavMesh(routeImpasseNav);
        }
        else if (sorties == 1)
        {
            if (top)
            {
                SwapWith(routeImpasse);
                ActivateNavMesh(routeImpasseNav);
            }
            else if (left)
            {
                SwapWith(routeImpasse);
                ActivateNavMesh(routeImpasseNav);
                transform.Rotate(new Vector3(0, -90, 0));
            }
            else if (bot)
            {
                SwapWith(routeImpasse);
                ActivateNavMesh(routeImpasseNav);
                transform.Rotate(new Vector3(0, 180, 0));
            }
            else
            {
                SwapWith(routeImpasse);
                ActivateNavMesh(routeImpasseNav);
                transform.Rotate(new Vector3(0, 90, 0));
            }
        }
        else if (sorties == 2)
        {
            if (top)
            {
                if (bot)
                {
                    SwapWith(routeDroite);
                    ActivateNavMesh(routeDroiteNav);
                }
                if (left)
                {
                    SwapWith(routeAngle);
                    ActivateNavMesh(routeAngleNav);
                }
                if (right)
                {
                    SwapWith(routeAngle);
                    ActivateNavMesh(routeAngleNav);
                    transform.Rotate(new Vector3(0, 90, 0));
                }
            }
            else if (left && !top)
            {
                if (right)
                {
                    SwapWith(routeDroite);
                    ActivateNavMesh(routeDroiteNav);
                    transform.Rotate(new Vector3(0, 90, 0));
                }
                if (bot)
                {
                    SwapWith(routeAngle);
                    ActivateNavMesh(routeAngleNav);
                    transform.Rotate(new Vector3(0, -90, 0));
                }
            }
            else if (bot)
            {
                if (right)
                {
                    SwapWith(routeAngle);
                    ActivateNavMesh(routeAngleNav);
                    transform.Rotate(new Vector3(0, 180, 0));
                }
            }
        }
        else if (sorties == 3)
        {
            if (!top)
            {
                SwapWith(route3sortie);
                ActivateNavMesh(route3sortiesNav);
                transform.Rotate(new Vector3(0, -90, 0));
            }
            else if (!left)
            {
                SwapWith(route3sortie);
                ActivateNavMesh(route3sortiesNav);
                transform.Rotate(new Vector3(0, 180, 0));
            }
            else if (!bot)
            {
                SwapWith(route3sortie);
                ActivateNavMesh(route3sortiesNav);
                transform.Rotate(new Vector3(0, 90, 0));
            }
            else
            {
                SwapWith(route3sortie);
                ActivateNavMesh(route3sortiesNav);
            }
        }
        else
        {
            SwapWith(route4sorties);
            ActivateNavMesh(route4sortiesNav);
        }
    }

    public int NombresDeSorties()
    {
        int sorties = 0;

        if (a.PosX >= 0 && a.PosY >= 0 && a.PosX < ville.Map.Taille && a.PosY < ville.Map.Taille)
        {
            if (a.PosX > 0)
            {
                if (ville.Map.Carte[a.PosY, a.PosX - 1] != null && ville.Map.Carte[a.PosY, a.PosX - 1] is Route)
                {
                    sorties++;
                    top = true;
                }
            }
            if (a.PosY > 0)
            {
                if (ville.Map.Carte[a.PosY - 1, a.PosX] != null && ville.Map.Carte[a.PosY - 1, a.PosX] is Route)
                {
                    sorties++;
                    left = true;
                }
            }
            if (a.PosX < ville.Map.Taille - 1)
            {
                if (ville.Map.Carte[a.PosY, a.PosX + 1] != null && ville.Map.Carte[a.PosY, a.PosX + 1] is Route)
                {
                    sorties++;
                    bot = true;
                }
            }
            if (a.PosY < ville.Map.Taille - 1)
            {
                if (ville.Map.Carte[a.PosY + 1, a.PosX] != null && ville.Map.Carte[a.PosY + 1, a.PosX] is Route)
                {
                    sorties++;
                    right = true;
                }
            }
        }

        return sorties;
    }

    void SwapWith(GameObject ga)
    {
        GetComponent<MeshFilter>().sharedMesh = ga.GetComponent<MeshFilter>().sharedMesh;
    }

    void ActivateNavMesh(string navMeshName)
    {
        transform.Find(navMeshName).gameObject.SetActive(true);
    }

    void DesactivateAllChildren()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
