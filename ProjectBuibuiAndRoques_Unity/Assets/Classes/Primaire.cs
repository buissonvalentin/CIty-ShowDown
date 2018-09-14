using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class Primaire : Batiment, IEmployable
    {
        private int nourritureProduite;
        private int nourritureRestante;
        int productionMin;
        int productionMax;
        int nbrEmployeActuelAise;
        int nbrEmployeActuelMoyenne;
        int nbrEmployeActuelOuvriere;
        int nbrEmployeMaxAise;
        int nbrEmployeMaxMoyenne;
        int nbrEmployeMaxOuvriere;
        float coefOccupation;
        public Primaire(int nbrEmployeMaxOuvriere, int nbrEmployeMaxMoyenne, int nbrEmployeMaxAise, int productionMax, int coutMensuel, string nom, int prix, int taille, int niveau) : base(0.4f, 1.1f, coutMensuel, nom, prix, taille, niveau)
        {
            this.nbrEmployeMaxOuvriere = nbrEmployeMaxOuvriere;
            this.nbrEmployeMaxMoyenne = nbrEmployeMaxMoyenne;
            this.nbrEmployeMaxAise = nbrEmployeMaxAise;
            this.nourritureProduite = productionMax / 2;
            this.nourritureRestante = 0;
            this.productionMin = 0;
            this.productionMax = productionMax;
            coefOccupation = 0;
        }

        public bool estFonctionnel()
        {
            return (EstConnecte);//&& nbrEmployeActuelAise >= NbrEmployeMaxAise / 2 && nbrEmployeActuelMoyenne >= NbrEmployeMaxMoyenne / 2 && nbrEmployeActuelOuvriere >= NbrEmployeMaxOuvriere / 2);
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
        
        public int NourritureProduite
        {
            get
            {
                return nourritureProduite;
            }

            set
            {
                nourritureProduite = value;
            }
        }

        public int NourritureRestante
        {
            get
            {
                return nourritureRestante;
            }

            set
            {
                nourritureRestante = value;
            }
        }

        public int ProductionMin
        {
            get
            {
                return productionMin;
            }

            set
            {
                productionMin = value;
            }
        }

        public int ProductionMax
        {
            get
            {
                return productionMax;
            }

            set
            {
                productionMax = value;
            }
        }

        public int NourritureUtilisee(int nourritureNecessaire)
        {
            int nourritureRenvoyee = nourritureNecessaire - nourritureProduite;
            if (nourritureRenvoyee < 0)
            {
                nourritureRestante = -nourritureRenvoyee;
                nourritureRenvoyee = 0;
            }
            else
            {
                nourritureRestante = 0;
            }
            return nourritureRenvoyee;
        }

        public override string ToString()
        {
            return base.ToString()+"\nNombre d'employé ($$$-$$-$) : \n" + nbrEmployeActuelAise + "/" + nbrEmployeMaxAise + " - " + nbrEmployeActuelMoyenne + "/" + nbrEmployeMaxMoyenne + " - " + nbrEmployeActuelOuvriere + "/" + nbrEmployeMaxOuvriere;
        }
        public override string AffichageAchat()
        {
            return base.ToString() + "\nProduction Max : " + productionMax;
        }
    }
}
