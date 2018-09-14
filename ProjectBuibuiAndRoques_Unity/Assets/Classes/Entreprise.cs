using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    abstract class Entreprise : Secondaire, IEmployable
    {
        int nbrEmployeActuelAise;
        int nbrEmployeActuelMoyenne;
        int nbrEmployeActuelOuvriere;
        int nbrEmployeMaxAise;
        int nbrEmployeMaxMoyenne;
        int nbrEmployeMaxOuvriere;
        float coefOccupation;

        public Entreprise(int nbrEmployeMaxAise, int nbrEmployeMaxMoyenne, int nbrEmployeMaxOuvriere, int coutMensuel, string nom, int prix, int taille, int niveau) : base(coutMensuel, nom, prix, taille, niveau)
        {
            this.nbrEmployeActuelAise = 0;
            this.nbrEmployeActuelMoyenne = 0;
            this.nbrEmployeActuelOuvriere = 0;
            this.nbrEmployeMaxAise = nbrEmployeMaxAise;
            this.nbrEmployeMaxMoyenne = nbrEmployeMaxMoyenne;
            this.nbrEmployeMaxOuvriere = nbrEmployeMaxOuvriere;
            coefOccupation = 0;
        }
        public Entreprise(float coefAttractivite, float coefCulture, int nbrEmployeMaxAise, int nbrEmployeMaxMoyenne, int nbrEmployeMaxOuvriere, int coutMensuel, string nom, int prix, int taille, int niveau) : base(coefAttractivite, coefCulture, coutMensuel, nom, prix, taille, niveau)
        {
            this.nbrEmployeActuelAise = 0;
            this.nbrEmployeActuelMoyenne = 0;
            this.nbrEmployeActuelOuvriere = 0;
            this.nbrEmployeMaxAise = nbrEmployeMaxAise;
            this.nbrEmployeMaxMoyenne = nbrEmployeMaxMoyenne;
            this.nbrEmployeMaxOuvriere = nbrEmployeMaxOuvriere;
            coefOccupation = 0;
        }

        public bool estFonctionnel()
        {
            return (EstConnecte);// && nbrEmployeActuelAise >= NbrEmployeMaxAise / 2 && nbrEmployeActuelMoyenne >= NbrEmployeMaxMoyenne / 2 && nbrEmployeActuelOuvriere >= NbrEmployeMaxOuvriere / 2);
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

        public override string ToString()
        {
            return base.ToString()+"\nNombre d'employé ($$$-$$-$) : \n"+nbrEmployeActuelAise+"/"+nbrEmployeMaxAise+" - "+nbrEmployeActuelMoyenne+"/"+nbrEmployeMaxMoyenne+" - "+nbrEmployeActuelOuvriere+"/"+nbrEmployeMaxOuvriere;
        }
        public override string AffichageAchat()
        {
            return base.AffichageAchat();
        }
        
    }
}
