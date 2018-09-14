using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class CompagnieEau : Entreprise, IParametrable
    {
        private int eauProduite;
        private int eauRestante;
        int productionMin;
        int productionMax;



        public CompagnieEau(int productionMax, int NbrEmployeMaxAise, int NbrEmployeMaxMoyenne, int NbrEmployeMaxOuvriere, int coutMensuel, string nom, int prix, int taille, int niveau) : base(0.4f, 1.1f, NbrEmployeMaxAise, NbrEmployeMaxMoyenne, NbrEmployeMaxOuvriere, coutMensuel, nom, prix, taille, niveau)
        {
            this.eauProduite = productionMax / 2;
            this.eauRestante = 0;
            this.productionMax = productionMax;
            productionMin = 0;
        }

        public override string ToString()
        {
            return base.ToString();
        }
        public override string AffichageAchat()
        {
            return base.AffichageAchat() + "\nEau max produite : " + productionMax;
        }

        /// <summary>
        /// Retourne l'eau encore nécessaire à prélever dans d'autres compagnies
        /// </summary>
        /// <param name="eauNecessaire">Prend en paramètre l'eau nécessaire et déduit l'eau produite par la pompe</param>
        /// <returns></returns>
        public int EauUtilisee(int eauNecessaire)
        {
            int eauRenvoyee = eauNecessaire - eauProduite;
            if (eauRenvoyee < 0)
            {
                eauRestante = -eauRenvoyee;//Met en paramètre l'eau stocké qui ne servira plus à rien
                eauRenvoyee = 0;
            }
            else
            {
                eauRestante = 0;
            }
            return eauRenvoyee;
        }

        void AfficheSlideBar(double valueMin, double value, double valueMax, double interval)
        {
            string temp = valueMin + " <";
            for (double i = valueMin; i < value; i += interval)
            {
                temp += "-";
            }
            temp += value;
            for (double i = value; i < valueMax; i += interval)
            {
                temp += "-";
            }
            temp += "> " + valueMax;
            Console.WriteLine(temp);
        }

        public void ModifierParametre()
        {
            bool continuer = true;
            int interval = 100;
            Console.WriteLine("Eau produite par cycle : ");
            AfficheSlideBar(productionMin, eauProduite, productionMax, interval);

            while (continuer)
            {
                /*
                ConsoleKey key = Console.ReadKey(true).Key;

               
                if (key == ConsoleKey.RightArrow)
                {
                    eauProduite += interval;
                    if (eauProduite >= productionMax) eauProduite = productionMax;
                }
                if (key == ConsoleKey.LeftArrow)
                {
                    eauProduite -= interval;
                    if (eauProduite < productionMin) eauProduite = productionMin;
                }
                if (key == ConsoleKey.Escape)
                {
                    continuer = false;
                }
                */
                Console.WriteLine("Eau produite par cycle : ");
                AfficheSlideBar(productionMin, eauProduite, productionMax, interval);

            }
        }

        public int EauProduite
        {
            get
            {
                return eauProduite;
            }

            set
            {
                eauProduite = value;
            }
        }

        public int EauRestante
        {
            get
            {
                return eauRestante;
            }

            set
            {
                eauRestante = value;
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
    }
}
