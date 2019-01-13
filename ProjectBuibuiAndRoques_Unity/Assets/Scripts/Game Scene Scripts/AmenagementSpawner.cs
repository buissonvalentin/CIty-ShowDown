using ProjectRoquesAndBuiBui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmenagementSpawner : MonoBehaviour
{
    bool isSpawning = false;
    bool trumble = false;
    public float RumbleIntensity = 1;
    public float growingSpeed = 1;

    Vector3 origin;

    Transform amenagement;

    public GameObject particleSystemUnit;
    List<GameObject> listParticleSystem;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSpawning)
        {
            Trumble();
            Grow();
        }  
    }

    public void Spawn(Transform prefab)
    {
        //placer la source audio au bon endroit
        
        isSpawning = true;
        amenagement = prefab;
        origin = new Vector3(amenagement.position.x, amenagement.position.y, amenagement.position.z);
        transform.position = origin;

        // On place le batiment sous le terrain
        amenagement.position = new Vector3(amenagement.position.x, -20, amenagement.position.z);

        StartAudio();
        StartParticleEmition();
        
    }

    void EndSpawn()
    {
        StopAudio();
        RemoveParticle();
    }

    #region Prefab Position
    // Fait vibrer lateralemebt le batiment
    void Trumble()
    {
        trumble = !trumble;
        float moveX = Random.Range(-RumbleIntensity, RumbleIntensity);
        float moveZ = Random.Range(-RumbleIntensity, RumbleIntensity);

        if (trumble)
            amenagement.position += new Vector3(moveX, 0, moveZ);
        else
            amenagement.position = new Vector3(origin.x, amenagement.position.y, origin.z);
    }

    // Fait Monter le batiment
    void Grow()
    {
        amenagement.position += new Vector3(0, growingSpeed * Time.deltaTime, 0);
        if(amenagement.position.y >= 0)
        {
            amenagement.position = new Vector3(origin.x, 0, origin.z);
            EndSpawn();
            isSpawning = false;
        }
    }
    #endregion

    #region Particle System
    void StartParticleEmition()
    {
        listParticleSystem = new List<GameObject>();
        Amenagement amenTemp = amenagement.gameObject.GetComponent<AmenagementPrefab>().Amenagement;
        for (int i = amenTemp.PosX; i < amenTemp.PosX + amenTemp.Taille; i++)
        {
            for (int j = amenTemp.PosY; j < amenTemp.PosY + amenTemp.Taille; j++)
            {
                GameObject temp = Instantiate(particleSystemUnit);
                temp.SetActive(true);
                temp.transform.position = new Vector3(ConvertToMapScale(i), 0.1f, ConvertToMapScale(j));
                listParticleSystem.Add(temp);
            }
        }
    }

    void RemoveParticle()
    {
        foreach (GameObject go in listParticleSystem)
        {
            Destroy(go);
        }

    }
    #endregion

    #region Audio
    void StartAudio()
    {
        float volume = FindObjectOfType<Manager>().Settings.noiseVolume;
        audioSource.time = 0f;
        audioSource.volume = volume/100;
        audioSource.Play();
    }

    void StopAudio()
    {
        audioSource.Pause();
    }
    #endregion

    /// <summary>
    /// Renvoie la position sur le terrain en fonction de l'index dans le tableau
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    float ConvertToMapScale(int pos)
    {
        return pos * 10 + 5f;
    }


}
