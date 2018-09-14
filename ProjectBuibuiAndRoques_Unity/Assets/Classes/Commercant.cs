using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class Commercant : Tertiaire, IEmployable
    {
        int nbrEmployeActuelAise;
        int nbrEmployeActuelMoyenne;
        int nbrEmployeActuelOuvriere;
        int nbrEmployeMaxAise;
        int nbrEmployeMaxMoyenne;
        int nbrEmployeMaxOuvriere;
        int produitVendu;
        float prixVente;
        float coefOccupation;
        int prixOptimal;

        public Commercant(int nbrEmployeMaxAise, int nbrEmployeMaxMoyenne, int nbrEmployeMaxOuvriere, int coutMensuel, string nom, int prix, int taille, int niveau) : base(1.1f, 0.7f, coutMensuel, nom, prix, taille, niveau)
        {
            this.nbrEmployeActuelAise = 0;
            this.nbrEmployeActuelMoyenne = 0;
            this.nbrEmployeActuelOuvriere = 0;
            this.nbrEmployeMaxAise = nbrEmployeMaxAise;
            this.nbrEmployeMaxMoyenne = nbrEmployeMaxMoyenne;
            this.nbrEmployeMaxOuvriere = nbrEmployeMaxOuvriere;
            Random prixProduit = new Random();
            this.prixOptimal = prixProduit.Next(20, 40);
            produitVendu = 0;
            this.coefOccupation = 0;
            prixVente = 10;
        }

        public int NbrEmployeActuelAise
        {
            get
            {
                return nbrEmployeActuelAise;
            }

            set
            {
                nbrEmployeActuelAise = value;
            }
        }

        public int NbrEmployeActuelMoyenne
        {
            get
            {
                return nbrEmployeActuelMoyenne;
            }

            set
            {
                nbrEmployeActuelMoyenne = value;
            }
        }

        public int NbrEmployeActuelAise1
        {
            get
            {
                return nbrEmployeActuelAise;
            }

            set
            {
                nbrEmployeActuelAise = value;
            }
        }

        public int NbrEmployeActuelMoyenne1
        {
            get
            {
                return nbrEmployeActuelMoyenne;
            }

            set
            {
                nbrEmployeActuelMoyenne = value;
            }
        }

        public int NbrEmployeActuelOuvriere
        {
            get
            {
                return nbrEmployeActuelOuvriere;
            }

            set
            {
                nbrEmployeActuelOuvriere = value;
            }
        }

        public int NbrEmployeMaxAise
        {
            get
            {
                return nbrEmployeMaxAise;
            }

            set
            {
                nbrEmployeMaxAise = value;
            }
        }

        public int NbrEmployeMaxMoyenne
        {
            get
            {
                return nbrEmployeMaxMoyenne;
            }

            set
            {
                nbrEmployeMaxMoyenne = value;
            }
        }

        public int NbrEmployeMaxOuvriere
        {
            get
            {
                return nbrEmployeMaxOuvriere;
            }

            set
            {
                nbrEmployeMaxOuvriere = value;
            }
        }

        public float CoefOccupation
        {
            get
            {
                return coefOccupation;
            }

            set
            {
                coefOccupation = value;
            }
        }

        public int ProduitVendu
        {
            get
            {
                return produitVendu;
            }

            set
            {
                produitVendu = value;
            }
        }

        public float PrixVente
        {
            get
            {
                return prixVente;
            }

            set
            {
                prixVente = value;
            }
        }

        public int PrixOptimal
        {
            get
            {
                return prixOptimal;
            }

            set
            {
                prixOptimal = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() +"\nProduit vendu : "+this.produitVendu+"\nPrix vente : "+this.prixVente+ "\nNombre d'employé ($$$-$$-$) : \n" + nbrEmployeActuelAise + "/" + nbrEmployeMaxAise + " - " + nbrEmployeActuelMoyenne + "/" + nbrEmployeMaxMoyenne + " - " + nbrEmployeActuelOuvriere + "/" + nbrEmployeMaxOuvriere;
        }
        public override string AffichageAchat()
        {
            return base.AffichageAchat();
        }
    }
}
