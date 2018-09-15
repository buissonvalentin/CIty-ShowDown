using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjectRoquesAndBuiBui
{
    public class Amenagement
    {
        string nom;
        int idAmenagement;
        int prix;
        int taille;
        bool estConnecte; // true si le batiment est relié à la sortie de la ville par une route

        private static int globalIdAmenagement = 0;
        int posX;
        int posY;
        int niveau;

        public Amenagement(string nom, int prix, int taille, int niveau)
        {
            this.nom = nom;
            this.idAmenagement = globalIdAmenagement;
            this.prix = prix;
            this.taille = taille;
            this.niveau = niveau;
            estConnecte = false;
            globalIdAmenagement += 1;
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
    }
}
