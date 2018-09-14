using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ProjectRoquesAndBuiBui;


public enum Type
{
    BatimentAdministartif = 0,
    Bureau = 1,
    Commercant = 2,
    CompagnieEau = 3,
    CompagnieElectricite = 4,
    CompagnieTransport = 5,
    Culture = 6,
    Tourisme = 7,
    Logement = 8,
    Route = 9,
    Usine = 10,
    Primaire = 11
}

public class AmenagementPrefab : MonoBehaviour {

    #region Attributs
    public Type type;

    //Amenagement
    public string nom;
    public int prix;
    public int taille;
    public int niveau;

    
    //Batiment
    public int coutMensuel;

    //Batiment administratif
    public int nombreHabitantNecessaire;

    //Bureau
    public int placesDisponible;
    public int minOccupation;
    public float prixLocation;

    //Commercant


    //IEmployable
    public int nbrEmployeMaxAise;
    public int nbrEmployeMaxMoyenne;
    public int nbrEmployeMaxOuvriere;

    //Eau/elec employable
    public int productionMax;

    //transport
    // 0 partout

    //culture
    public int niveauCulture;

    // tourisme
    public int impactTourisme;

    // ENtreprise, Usine == employable

    //logement
    public int capaciteMax;
    public ClasseSocial classe;
    public float nivBonheur;

    //route
    public bool estSortie;
    

    #endregion

    Amenagement amenagement;
    void Start () {
        if(amenagement == null)
        {
            amenagement = CreationAmenagement();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Amenagement CreationAmenagement()
    {
        Amenagement temp = null;
        
        if(type == Type.BatimentAdministartif)
        {
            temp = new BatimentAdministratif(nombreHabitantNecessaire, coutMensuel, nom, prix, taille, niveau);
        }
        else if(type == Type.Bureau)
        {
            temp = new Bureau(placesDisponible, prixLocation, coutMensuel, nom, prix, taille, niveau);
        }
        else if(type == Type.Commercant)
        {
            temp = new Commercant(nbrEmployeMaxAise, nbrEmployeMaxMoyenne, nbrEmployeMaxOuvriere, coutMensuel, nom, prix, taille, niveau);
        }
        else if(type == Type.CompagnieEau)
        {
            temp = new CompagnieEau(productionMax, nbrEmployeMaxAise, nbrEmployeMaxMoyenne, nbrEmployeMaxOuvriere, coutMensuel, nom, prix, taille, niveau);
        }
        else if(type == Type.CompagnieElectricite)
        {
            temp = new CompagnieElectricite(productionMax, nbrEmployeMaxAise, nbrEmployeMaxMoyenne, nbrEmployeMaxOuvriere, coutMensuel, nom, prix, taille, niveau);
        }
        else if(type == Type.CompagnieTransport)
        {
            temp = new CompagnieTransport(coutMensuel, nom, prix, taille, niveau);
        }
        else if(type == Type.Culture)
        {
            temp = new Culture(niveauCulture, coutMensuel, nom, prix, taille, niveau);
        }
        else if(type == Type.Tourisme)
        {
            temp = new Tourisme(impactTourisme, coutMensuel, nom, prix, taille, niveau);
        }
        else if(type == Type.Logement)
        {
            temp = new Logement(capaciteMax, classe, nivBonheur, coutMensuel, nom, prix, taille, niveau);
        }
        else if(type == Type.Route)
        {
            temp = new Route(nom, prix, taille, estSortie);
        }
        else if(type == Type.Usine)
        {
            temp = new Usine(nbrEmployeMaxAise, nbrEmployeMaxMoyenne, nbrEmployeMaxOuvriere, coutMensuel, nom, prix, taille, niveau);
        }
        else if(type == Type.Primaire)
        {
            temp = new Primaire(nbrEmployeMaxAise, nbrEmployeMaxMoyenne, nbrEmployeMaxOuvriere, productionMax, coutMensuel, nom, prix, taille, niveau);
        }
        
        return temp;
    }

    public virtual  Amenagement Amenagement
    {
        get
        {
            if(amenagement == null)
            {
                amenagement = CreationAmenagement();
            }
            return amenagement; 
        }
        set
        {
            amenagement = value;
        }
    }

}

