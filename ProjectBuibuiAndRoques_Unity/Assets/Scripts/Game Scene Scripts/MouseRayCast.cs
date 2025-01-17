﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using ProjectRoquesAndBuiBui;
using System;

public class MouseRayCast : MonoBehaviour {

    GameObject GOAmenagement;
    GameObject modelActuel;
    Amenagement amenagement;
    Vector3 rotation;

    Ville ville;
    public Menu menu;
    public GameObject boxPause;

    // Cursor Textures
    public Texture2D cursorDeplacementCamera;
    public Texture2D cursorOrientationCamera;
    public Texture2D cursorDestroy;
    bool isCursorUsed;
    
    // Destruction Mode building management
    Transform buildingTextureHolder;
    Transform targetBuldingDestroy;

    Transform targetBuilding;

    public Material indisponibleMat;
    public Material disponibleMat;

    // Road placement
    public GameObject boule;
    GameObject firstBoule;
    GameObject secondBoule;

    // Positionning
    float posX;
    float posZ;
    int gridX;
    int gridZ;
    float x;
    float z;

    public float Zoomsensitivity = 10f;
    float moveSensitivity = 1;

    bool dispo;
    bool hitTerrain;
    bool isInDestroyMode;
    RaycastHit hit;

    private Vector3 dragOrigin;

    // Outline
    public GameObject unitOutline;
    List<GameObject> outlineGroup;

    //Amenagement Spawner
    public GameObject amenagementSpawner;

    Settings settings;
    

    void Start () {
        isInDestroyMode = false;
        posX = 0;
        posZ = 0;
        gridX = 0;
        gridZ = 0;
        x = 0;
        z = 0;
        ville = FindObjectOfType<GameManager>().ville;
        UpdateSettings();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        isCursorUsed = false;
        dispo = false;
        hitTerrain = false;

        RayCast();

        DeplaceAmenagement();

        RotationBatiment();

        GestionClickGauche();

        GestionClickDroit();

        GestionEscape();

        MoveCamera();

        OrientationCamera();

        ZoomCamera();

        GestionDestructionMode();

        CorrectionCameraPosition();

        CheckForShortCut();

        if (targetBuilding != null)
            menu.AfficheInfoAmenagement(targetBuilding);

        if (!isCursorUsed)
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void CheckForShortCut()
    {
        if (Input.GetKeyDown(settings.shortcutKeyRotate)){
            if(targetBuilding != null)
            {
                targetBuilding.Rotate(new Vector3(0, 90f, 0));
            }
        }

        if (Input.GetKeyDown(settings.shortcutKeyDelete))
        {
            ToggleDestroyMode();
        }

        if (Input.GetKey(settings.shortcutKeyForward))
        {
            transform.position += Vector3.left * settings.moveSensibility;
        }
        else if (Input.GetKey(settings.shortcutKeyBackward))
        {
            transform.position += Vector3.right * settings.moveSensibility;
        }
        else if (Input.GetKey(settings.shortcutKeyRight))
        {
            transform.position += Vector3.forward * settings.moveSensibility;
        }
        else if (Input.GetKey(settings.shortcutKeyLeft))
        {
            transform.position += Vector3.back * settings.moveSensibility;
        }

    }

    public void SetAmenagement(GameObject a)
    { 
        isInDestroyMode = false;
        Destroy(GOAmenagement);
        amenagement = null;
        modelActuel = a;
        GOAmenagement = Instantiate(modelActuel, new Vector3(posX, 0.1f, posZ), Quaternion.identity);
        amenagement = GOAmenagement.GetComponent<AmenagementPrefab>().Amenagement;
    }

    void RayCast()
    {
        
        if (GOAmenagement != null)
        {
            int mask = LayerMask.GetMask("Default");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 5000f, mask))
            {
                hitTerrain = true;
                posX = hit.point.x;
                posZ = hit.point.z;
                x = Mathf.FloorToInt(posX / 10) * 10 + 5f;
                z = Mathf.FloorToInt(posZ / 10) * 10 + 5f;
                gridX = Mathf.FloorToInt(x) / 10;
                gridZ = Mathf.FloorToInt(z) / 10;
            }
        }
            
    }

    void MoveCamera()
    {
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(2))
        {
            return;
        }

        Cursor.SetCursor(cursorDeplacementCamera, Vector2.zero, CursorMode.Auto);
        isCursorUsed = true;
        transform.Translate((dragOrigin - Input.mousePosition) * transform.position.y/500);
        dragOrigin = Input.mousePosition;
    }

    void ZoomCamera()
    {
        if(!boxPause.activeInHierarchy)
            transform.position +=  transform.forward * (Input.GetAxis("Mouse ScrollWheel") * (transform.position.y/5) * Zoomsensitivity);
        
    }

    void CorrectionCameraPosition()
    {
        float maxHeigth = 300;
        if (transform.position.x < 0) transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        if (transform.position.x >= 1200) transform.position = new Vector3(1200f, transform.position.y, transform.position.z);
        if (transform.position.y < 0) transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        if (transform.position.y >= maxHeigth) transform.position = new Vector3(transform.position.x, maxHeigth, transform.position.z);
        if (transform.position.z < 0) transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (transform.position.z >= 1200) transform.position = new Vector3(transform.position.x, transform.position.y, 1200f);

    }

    void DeplaceAmenagement()
    {
        if (GOAmenagement != null) // placement d'un amenagement sur la carte
        {
            
            if (amenagement != null) // vérifie que l'amenagement à bien été instantié dans le GameObject
            {

                amenagement.PosX = gridX;
                amenagement.PosY = gridZ;
                dispo = ville.Map.VerifierPlace(amenagement);

                // Modifie le material du batiment pour afficher si la place est disponible
                if (dispo)
                {
                    ChangeGameObjectTexture(GOAmenagement, disponibleMat);
                }
                else
                {
                    ChangeGameObjectTexture(GOAmenagement, indisponibleMat);
                }

                // positionnement du prefab par rapport au curseur de la souris
                if (amenagement.Taille%2 == 0)
                {
                    GOAmenagement.transform.position = new Vector3(x + (amenagement.Taille - 1) * 5f, 0.1f, z + (amenagement.Taille - 1) * 5f);
                }
                else
                {
                    GOAmenagement.transform.position = new Vector3(x + Mathf.Max(0, amenagement.Taille - 2) * 10f , 0.1f, z + Mathf.Max(0, amenagement.Taille - 2) * 10f);
                }

                GOAmenagement.transform.rotation = Quaternion.Euler(rotation);
            }
            else
            {
                amenagement = GOAmenagement.GetComponent<AmenagementPrefab>().Amenagement;
            }

        }
        else
        {
            Cursor.visible = true;
        }
    }

    void RotationBatiment()
    {
        Settings set = FindObjectOfType<Manager>().Settings;
        if (Input.GetKeyDown(set.shortcutKeyRotate))
        {
            if(GOAmenagement != null)
            {
                rotation += new Vector3(0, 90f, 0);
            }
        }
    }

    void GestionClickGauche()
    {
        if (Input.GetMouseButton(0) && hitTerrain)  // click gauche souris
        {
            
            if (GOAmenagement != null && amenagement != null && !EventSystem.current.IsPointerOverGameObject()) // place le batiment attaché au curseur du joueur
            {
                if(amenagement is Route)
                {
                    if (firstBoule == null)
                    {
                        firstBoule = Instantiate(boule, new Vector3(x, 5f, z), Quaternion.identity);
                        GOAmenagement.SetActive(false);
                        secondBoule = Instantiate(boule, new Vector3(x, 5f, z), Quaternion.identity);
                    }
                    secondBoule.transform.position = new Vector3(x , 5f , z);

                }
                else
                {
                    if (dispo && ville.PlacerUnAmenagement(amenagement)) // placement du batiment sur la carte
                    {
                        ChangeGameObjectTextureToOriginal(GOAmenagement, modelActuel);
                        amenagementSpawner.GetComponent<AmenagementSpawner>().Spawn(GOAmenagement.transform);

                        // on place le model attaché à la souris en remplacant le model attaché a la souris par un nouveau, l'autre est donc laissé sur place
                        GOAmenagement = Instantiate(modelActuel, new Vector3(posX, 0, posZ), Quaternion.identity);
                        amenagement = GOAmenagement.GetComponent<AmenagementPrefab>().Amenagement;
                        amenagement.Rotation = rotation;
                    }
                }
                

            }
            
        }

        // Placement des route en gardant le clique enfoncé
        if (Input.GetMouseButtonUp(0) && hitTerrain) 
        {
            
            bool ligneDispo = true;
            GOAmenagement.SetActive(true);
            
            if (firstBoule != null && firstBoule.transform.position.x  == x)
            {
                for(int i = Mathf.Min(Mathf.FloorToInt(firstBoule.transform.position.z) / 10, gridZ) ; i <= Mathf.Max(gridZ, Mathf.FloorToInt(firstBoule.transform.position.z) / 10) ; i++)
                {
                    if (!ville.Map.VerifierPlace(new Route("", 0, 1, Mathf.FloorToInt(x) / 10, i, false)) && !(ville.Map.Carte[i, Mathf.FloorToInt(x) / 10] is Route))
                    {
                        ligneDispo = false;
                    }
                }
                if (ligneDispo)
                {
                    Destroy(GOAmenagement);
                    for (int i = Mathf.Min(Mathf.FloorToInt(firstBoule.transform.position.z) / 10, gridZ); i <= Mathf.Max(gridZ, Mathf.FloorToInt(firstBoule.transform.position.z) / 10); i++)
                    {
                        if (ville.PlacerUnAmenagement(new Route("", 0, 1, Mathf.FloorToInt(x) / 10, i, false)))
                        {
                            
                            GOAmenagement = Instantiate(modelActuel, new Vector3(ConvertToMapScale(gridX), 0.1f, ConvertToMapScale(i)), Quaternion.identity);
                            
                            amenagement = GOAmenagement.GetComponent<AmenagementPrefab>().Amenagement;
                            amenagement.PosX = gridX;
                            amenagement.PosY = i;
                            
                            
                        }
                    }
                }
                GOAmenagement = Instantiate(modelActuel, new Vector3(0, 0.1f, 0), Quaternion.identity);
                amenagement = GOAmenagement.GetComponent<AmenagementPrefab>().Amenagement;

            }
            else if(firstBoule != null && firstBoule.transform.position.z == z)
            {
                for (int i = Mathf.Min(Mathf.FloorToInt(firstBoule.transform.position.x) / 10, gridX); i <= Mathf.Max(gridX, Mathf.FloorToInt(firstBoule.transform.position.x) / 10) ; i++)
                {
                    if (!ville.Map.VerifierPlace(new Route("", 0, 1, i, Mathf.FloorToInt(z) / 10, false)) && !(ville.Map.Carte[Mathf.FloorToInt(z) / 10, i] is Route))
                    {
                        ligneDispo = false;
                    }
                }
                if (ligneDispo)
                {
                    Destroy(GOAmenagement);
                    for (int i = Mathf.Min(Mathf.FloorToInt(firstBoule.transform.position.x) / 10, gridX); i <= Mathf.Max(gridX, Mathf.FloorToInt(firstBoule.transform.position.x) / 10); i++)
                    {
                        if (ville.PlacerUnAmenagement(new Route("", 0, 1, i, Mathf.FloorToInt(z) / 10, false)))
                        {

                            GOAmenagement = Instantiate(modelActuel, new Vector3(ConvertToMapScale(i), 0.1f, ConvertToMapScale(gridZ)), Quaternion.identity);
                            amenagement = GOAmenagement.GetComponent<AmenagementPrefab>().Amenagement;
                            amenagement.PosX = i;
                            amenagement.PosY = gridZ;
                        }
                    }
                    GOAmenagement = Instantiate(modelActuel, new Vector3(0, 0.1f, 0), Quaternion.identity);
                    amenagement = GOAmenagement.GetComponent<AmenagementPrefab>().Amenagement;
                }
                
            }
            Destroy(firstBoule);
            Destroy(secondBoule);
            GOAmenagement.SetActive(true);
            secondBoule = null;
            firstBoule = null;
        }

        //Selection ou desruction d'un batiment
        if(Input.GetMouseButtonDown(0) && GOAmenagement == null) 
        {
            
            int mask = LayerMask.GetMask("Batiment"); 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool touch = Physics.Raycast(ray, out hit, 5000f, mask);

            if (touch && !EventSystem.current.IsPointerOverGameObject()) 
            {
                
                targetBuilding = hit.transform;
                if (!targetBuilding.GetComponent<AmenagementPrefab>())
                    targetBuilding = targetBuilding.parent;


                if (isInDestroyMode)
                {
                    if(targetBuldingDestroy != null)
                    {
                        Amenagement temp = targetBuilding.GetComponent<AmenagementPrefab>().Amenagement;
                        ville.SupprimerAmenagement(temp);
                        Destroy(targetBuilding.gameObject);
                        targetBuilding = null;
                        Destroy(buildingTextureHolder.gameObject);
                    }
                    
                }
                else // Le joueur a cliqué sur un batilent 
                {
                    // On affiche les informations du batiments
                    menu.AfficheInfoAmenagement(targetBuilding);
                    //Affiche l'outline du batiment
                    DisplayOutlineSelectedBuilding();
                }
                    
            }
            else if(!EventSystem.current.IsPointerOverGameObject()) // CLique sur un point de la map qui ne contient pas de batiment
            {
                // on cache les infos du batiment
                // on cache l'outline du batiment
                targetBuilding = null;
                RemoveOutlineSelectedTransform();
                menu.CacherInfoAmenagement();
            }
                
            
        }
    }

    void GestionClickDroit()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (GOAmenagement != null && modelActuel != null)
            {
                DestroyImmediate(GOAmenagement);
                GOAmenagement = null;
                modelActuel = null;
                amenagement = null;
            }
            if (isInDestroyMode)
            {
                isInDestroyMode = false;
            }
        }
        
    }

    void GestionEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // retire le batiment du curseur du joueur
        {
            if (GOAmenagement != null && modelActuel != null)
            {
                DestroyImmediate(GOAmenagement);
                GOAmenagement = null;
                modelActuel = null;
                amenagement = null;
            }
            else if (isInDestroyMode)
            {
                isInDestroyMode = false;
            }
            else
            {
                // toggle para menu
                if (boxPause.gameObject.activeInHierarchy)
                {
                    boxPause.GetComponent<PauseBox>().Close();
                    boxPause.SetActive(false);
                    
                }
                else
                {
                    boxPause.SetActive(true);
                }
            }

        }
    }

    void GestionDestructionMode()
    {
        if (isInDestroyMode)
        {
            
            int mask = LayerMask.GetMask("Batiment");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 5000f, mask))
            {

                if  (targetBuldingDestroy != hit.transform && targetBuldingDestroy != hit.transform.parent)
                {
                    ChangeGameObjectTextureToOriginal(targetBuldingDestroy.gameObject, buildingTextureHolder.gameObject);
                    Destroy(buildingTextureHolder.gameObject);
                    targetBuldingDestroy = null;
                }

                if(targetBuldingDestroy == null)
                {
                    targetBuldingDestroy = hit.transform;
                    if (!targetBuldingDestroy.GetComponent<AmenagementPrefab>())
                        targetBuldingDestroy = targetBuilding.parent;

                    buildingTextureHolder = Instantiate(targetBuldingDestroy);
                    buildingTextureHolder.gameObject.SetActive(false);


                    ChangeGameObjectTexture(targetBuldingDestroy.gameObject, indisponibleMat);
                }
                
                
            }
            else
            {
                if(targetBuldingDestroy != null)
                {
                    ChangeGameObjectTextureToOriginal(targetBuldingDestroy.gameObject, buildingTextureHolder.gameObject);
                    Destroy(buildingTextureHolder.gameObject);
                    targetBuldingDestroy = null;
                }
                    
            }
            
            Cursor.SetCursor(cursorDestroy, new Vector2(cursorDestroy.width/2, cursorDestroy.height/2) , CursorMode.Auto);
            isCursorUsed = true;
        }
        
    }

    void OrientationCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(1))
        {
            return;
        }

        Cursor.SetCursor(cursorOrientationCamera, Vector2.zero, CursorMode.Auto);
        isCursorUsed = true; 
        transform.Rotate(Vector3.up, -(dragOrigin.x - Input.mousePosition.x) * settings.orientationSensibility / 5, Space.World);
        transform.Rotate(transform.right, (dragOrigin.y - Input.mousePosition.y) * settings.orientationSensibility / 5, Space.World);
        dragOrigin = Input.mousePosition;
    }

    public void ToggleDestroyMode()
    {
        isInDestroyMode = !isInDestroyMode;
    }

    void ChangeGameObjectTexture(GameObject go, Material mat)
    {
        if (!go.GetComponent<Renderer>())
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                go.transform.GetChild(i).GetComponent<Renderer>().sharedMaterial = mat;
            }
        }
        else
            go.GetComponent<Renderer>().sharedMaterial = mat;

    }

    void ChangeGameObjectTextureToOriginal(GameObject go, GameObject original)
    {
        if (!go.GetComponent<Renderer>())
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                go.transform.GetChild(i).GetComponent<Renderer>().sharedMaterial = original.transform.GetChild(i).GetComponent<Renderer>().sharedMaterial;
            }
        }
        else
            go.GetComponent<Renderer>().sharedMaterial = original.GetComponent<Renderer>().sharedMaterial;
    }

    /// <summary>
    /// Place un carré jaune sous le batiment sélectionné
    /// </summary>
    void DisplayOutlineSelectedBuilding() 
    {
        outlineGroup = new List<GameObject>();
        Amenagement amenTemp = targetBuilding.gameObject.GetComponent<AmenagementPrefab>().Amenagement;
        for (int i = amenTemp.PosX; i < amenTemp.PosX + amenTemp.Taille; i++)
        {
            for(int j = amenTemp.PosY; j < amenTemp.PosY + amenTemp.Taille; j++)
            {
                GameObject temp = Instantiate(unitOutline);
                temp.transform.position = new Vector3(ConvertToMapScale(i), 0.1f, ConvertToMapScale(j));
                outlineGroup.Add(temp);
            }
        }
    }

    /// <summary>
    /// Retire le carré jaune placé sous le batiment sélectionné
    /// </summary>
    void RemoveOutlineSelectedTransform()
    {
        if(outlineGroup != null)
            foreach(GameObject go in outlineGroup)
            {
                Destroy(go);
            }

    }

    /// <summary>
    /// Renvoie la position sur le terrain en fonction de l'index dans le tableau
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    float ConvertToMapScale(int pos)
    {
        return pos * 10 + 5f;
    }

    public void UpdateSettings()
    {
        settings = FindObjectOfType<Manager>().Settings;
    }

    private void OnDrawGizmos()
    {
        if (firstBoule != null)
        {
            if(firstBoule.transform.position.x == x || firstBoule.transform.position.z == z)
            {
                Gizmos.color = Color.blue;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            
            Gizmos.DrawLine(firstBoule.transform.position, new Vector3(x, 5f, z));
        }
    }
}
