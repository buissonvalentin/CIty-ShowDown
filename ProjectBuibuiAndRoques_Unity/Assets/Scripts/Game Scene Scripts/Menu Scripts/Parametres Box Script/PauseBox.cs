using Assets.Scripts.Others;
using ProjectRoquesAndBuiBui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseBox : MonoBehaviour
{
    public Button continuer;
    public Button settings;
    public Button save;
    public Button quitter;

    public GameObject settingBox;

    // Start is called before the first frame update
    void Start()
    {
        continuer.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        settings.onClick.AddListener(() =>
        {
            settingBox.SetActive(true);
        });

        save.onClick.AddListener(() =>
        {
            Ville v = FindObjectOfType<GameManager>().ville;
            //v.Pause();
            v.TempsRestant = FindObjectOfType<Timer>().time;
            SavesManager.SaveGame(v);
            //v.Play();
        });

        quitter.onClick.AddListener(() =>
        {

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        settingBox.SetActive(false);
    }
}
