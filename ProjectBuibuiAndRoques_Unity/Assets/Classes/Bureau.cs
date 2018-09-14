using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class Bureau : Tertiaire
    {
        int maxPlaceDispo;
        int placesOccupees;
        int minOccupation;
        float coefInstallation;
        int placesDisponible;
        float prixLocation;
        public Bureau(int maxPlaceDispo, float prixLocation, int coutMensuel, string nom, int prix, int taille, int niveau) : base(1.1f, 0.5f, coutMensuel, nom, prix, taille, niveau)
        {
            this.maxPlaceDispo = maxPlaceDispo;
            this.placesOccupees = 0;
            this.placesDisponible = 0;
            this.minOccupation = MaxPlacesDisponible/2;
            this.coefInstallation = 1;
            this.prixLocation = prixLocation;
        }

        public int MinOccupation
        {
            get
            {
                return minOccupation;
            }

            set
            {
                minOccupation = value;
            }
        }

        public float CoefInstallation
        {
            get
            {
                return coefInstallation;
            }

            set
            {
                coefInstallation = value;
            }
        }

        public int PlacesOccupees
        {
            get
            {
                return placesOccupees;
            }

            set
            {
                placesOccupees = value;
            }
        }

        public int MaxPlacesDisponible
        {
            get
            {
                return maxPlaceDispo;
            }

            set
            {
                maxPlaceDispo = value;
            }
        }

        public float PrixLocation
        {
            get
            {
                return prixLocation;
            }

            set
            {
                prixLocation = value;
            }
        }

        public int PlacesDisponible
        {
            get
            {
                return placesDisponible;
            }

            set
            {
                placesDisponible = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + "\nPlaces occupées : " + placesOccupees + "\nPlaces disponibles : " + maxPlaceDispo + "\nMinimum d'occupation : " + minOccupation + "\nCoefficient de chance d'installation : " + coefInstallation + "\nPrix location : " + prixLocation;
        }
        public override string AffichageAchat()
        {
            return base.AffichageAchat() + "\nNombre de places : " + maxPlaceDispo + "\nPrix de location :" + prixLocation;
        }
    }
}
