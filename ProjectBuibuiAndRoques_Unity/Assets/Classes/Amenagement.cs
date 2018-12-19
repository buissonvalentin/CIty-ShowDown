using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ProjectRoquesAndBuiBui
{
    public class Amenagement
    {
        string type;
        string nom;
        int idAmenagement;
        int prix;
        int taille;
        Vector3 rotation;
        bool estConnecte; // true si le batiment est relié à la sortie de la ville par une route

        private static int globalIdAmenagement = 0;
        int posX;
        int posY;
        int niveau;

        public Amenagement(string nom, int prix, int taille, int niveau)
        {
            type = GetType().Name;
            this.nom = nom;
            this.idAmenagement = globalIdAmenagement;
            this.prix = prix;
            this.taille = taille;
            this.niveau = niveau;
            estConnecte = false;
            globalIdAmenagement += 1;
        }

        public Amenagement()
        {
        }

        public override string ToString()
        {
            return "";
        }

        public virtual string AffichageAchat()
        {
            return "";
        }


        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;
            }
        }

        public int IdAmenagement
        {
            get
            {
                return idAmenagement;
            }

            set
            {
                idAmenagement = value;
            }
        }

        public int Prix
        {
            get
            {
                return prix;
            }

            set
            {
                prix = value;
            }
        }

        public int Taille
        {
            get
            {
                return taille;
            }

            set
            {
                taille = value;
            }
        }

        public int PosX
        {
            get
            {
                return posX;
            }

            set
            {
                posX = value;
            }
        }

        public int PosY
        {
            get
            {
                return posY;
            }

            set
            {
                posY = value;
            }
        }

        public int Niveau
        {
            get
            {
                return niveau;
            }

            set
            {
                niveau = value;
            }
        }

        public bool EstConnecte
        {
            get
            {
                return estConnecte;
            }

            set
            {
                estConnecte = value;
            }
        }

        public Vector3 Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
