using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInventory : MonoBehaviour
{
    public GameObject logementOuvrierN1;
    public GameObject logementOuvrierN2;
    public GameObject logementOuvrierN3;
    public GameObject logemenMoyenN1A;
    public GameObject logemenMoyenN1B;
    public GameObject logemenMoyenN2;
    public GameObject logemenMoyenN3;
    public GameObject logementAiseN1;
    public GameObject logementAiseN3;
    public GameObject fermeN1;
    public GameObject fermeN2;
    public GameObject fermeN3;
    public GameObject eauN1;
    public GameObject eauN2;
    public GameObject energieN1;
    public GameObject energieN2;
    public GameObject energieN3;
    public GameObject transportN1;
    public GameObject usineN1;
    public GameObject usineN2;
    public GameObject usineN3;
    public GameObject commerceN1A;
    public GameObject commerceN1B;
    public GameObject commerceN2;
    public GameObject cultureN1A;
    public GameObject cultureN1B;
    public GameObject cultureN2A;
    public GameObject cultureN2B;
    public GameObject cultureN3A;
    public GameObject cultureN3B;
    public GameObject bureauN1;
    public GameObject bureauN2;
    public GameObject bureauN3;
    public GameObject tourismeN1A;
    public GameObject tourismeN1B;
    public GameObject tourismeN2A;
    public GameObject tourismeN2B;
    public GameObject tourismeN3A;
    public GameObject tourismeN3B;
    public GameObject tourismeN3C;
    public GameObject tourismeN25;
    public GameObject tourismeN30;
    public GameObject administration;
    public GameObject route;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPrefebBuilding(string name)
    {
        if (name == "maison d'ouvrier") return logementOuvrierN1;
        if (name == "Résidence Ouvrière") return logementOuvrierN2;
        if (name == "Immeuble d'Ouvriers") return logementOuvrierN3;
        if (name == "Maison Moyenne 2") return logemenMoyenN1A;
        if (name == "Maison Moyenne 3") return logemenMoyenN1B;
        if (name == "Résidence Moyenne") return logemenMoyenN2;
        if (name == "Immeuble Moyenne") return logemenMoyenN3;
        if (name == "Maison Aisée") return logementAiseN1;
        if (name == "Immeuble de Luxe") return logementAiseN3;
        if (name == "Ferme Bio") return fermeN1;
        if (name == "Ferme t'a Bouc Heu") return fermeN2;
        if (name == "Ferme Eau Jé Aime") return fermeN3;
        if (name == "Caffe") return commerceN1A;
        if (name == "Draugueux") return commerceN1B;
        if (name == "SuperMarché") return commerceN2;    
        if (name == "Tous Allo") return eauN1;
        if (name == "Chateau d'O") return eauN2;
        if (name == "Parc Eolienne") return energieN1;
        if (name == "Centrale à Charbon") return energieN2;
        if (name == "Centrale Nucléaire") return energieN3;
        if (name == "Parking") return transportN1;
        if (name == "Usine De recyclage") return usineN1;
        if (name == "Usine de Aime ST") return usineN2;
        if (name == "Usine A Monsieur Cocktail") return usineN3;
        if (name == "Merry Chrismas") return administration;
        if (name == "Grand Bureau") return bureauN1;
        if (name == "Moyen Bureau") return bureauN2;
        if (name == "Petit Bureau") return bureauN3;
        if (name == "Gym") return cultureN1A;
        if (name == "Peau R Noeud") return cultureN1B;
        if (name == "Cinéma") return cultureN2B;
        if (name == "Station Essence") return cultureN2A;
        if (name == "Ecole") return cultureN3A;
        if (name == "Eglise") return cultureN3B;
        if (name == "Maison Rhum Haine") return tourismeN1A;
        if (name == "Petit parc") return tourismeN1B;
        if (name == "Centre 2C Minaire") return tourismeN2A;
        if (name == "Foret") return tourismeN2B;
        if (name == "The Hempire S'Tête Bulle Ding ") return tourismeN3A;
        if (name == "Le Buisson") return tourismeN3B;
        if (name == "Le Roques") return tourismeN3C;
        if (name == "Groupe Hama Stade") return tourismeN25;
        if (name == "Chat Haut") return tourismeN30;
        if (name == "Route") return route;
        return null;
    }
}
