using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectRoquesAndBuiBui;
using System.Threading;

public class GameManager : MonoBehaviour {

    public Menu menu;

    // Use this for initialization
    public Ville ville;
    Thread gestion;

    void Awake () {
        ville = new Ville(15000000, 0, 0, 120, 2, 0.14f, 3, 450);
        gestion = new Thread(ville.EvolutionVariablesParTour);
        gestion.Start();

        Route sortie = new Route("Route", 0, 1, true);

        sortie.PosY = 41;
        sortie.PosX = 0;
        ville.PlacerUnAmenagement(sortie);
        
        //ville.Map.Carte[41, 0] = new Route("sortie", 0, 1, true);
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        menu.AfficheInfoVille(ville);
        menu.AfficheInfoPrincipales(ville);
        ville.CalculRessourcesConsomme();
        ville.CalculRessourcesProduite();
        ville.CalculLogements();

    }

    public void Pause()
    {
        ville.Pause();
    }

    public void Play()
    {
        ville.Play();
    }

    public void FinDePartie()
    {
        menu.AfficheBoxFinPartie(ville);
        ville.Pause();
    }

    public void LoadGame(Ville v)
    {
        ville = v;
        gestion = new Thread(ville.EvolutionVariablesParTour);
        gestion.Start();
    }

}
