using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class CompagnieElectricite : Entreprise
    {
        private int energieProduite;
        private int energieRestante;
        int productionMin;
        int productionMax;

        public int EnergieProduite
        {
            get
            {
                return energieProduite;
            }

            set
            {
                energieProduite = value;
            }
        }

        public int EnergieRestante
        {
            get
            {
                return energieRestante;
            }

            set
            {
                energieRestante = value;
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

        public CompagnieElectricite(int productionMax, int NbrEmployeMaxAise, int NbrEmployeMaxMoyenne, int NbrEmployeMaxOuvriere, int coutMensuel, string nom, int prix, int taille, int niveau) : base(0.4f, 1.1f, NbrEmployeMaxAise, NbrEmployeMaxMoyenne, NbrEmployeMaxOuvriere, coutMensuel, nom, prix, taille, niveau)
        {
            this.energieProduite = productionMax / 2;
            productionMax = 10000;
            productionMin = 0;
            this.productionMax = productionMax;
        }


        public override string ToString()
        {
            return base.ToString();
        }
        public override string AffichageAchat()
        {
            return base.AffichageAchat() + "\nEnergie max produite : " + productionMax;
        }

        public int EnergieUtilisee(int energieNecessaire)
        {
            int energieRenvoyee = energieNecessaire - energieProduite;
            if (energieRenvoyee < 0)
            {
                energieRestante = -energieRenvoyee;
                energieRenvoyee = 0;
            }
            else
            {
                energieRestante = 0;
            }
            return energieRenvoyee;
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
            AfficheSlideBar(productionMin, energieProduite, productionMax, interval);

            while (continuer)
            {
                


               // if (key == ConsoleKey.RightArrow)
                {
                    energieProduite += interval;
                    if (energieProduite >= productionMax) energieProduite = productionMax;
                }
               // if (key == ConsoleKey.LeftArrow)
                {
                    energieProduite -= interval;
                    if (energieProduite < productionMin) energieProduite = productionMin;
                }
                //if (key == ConsoleKey.Escape)
                {
                    continuer = false;
                }
                
                Console.WriteLine("Eau produite par cycle : ");
                AfficheSlideBar(productionMin, energieProduite, productionMax, interval);

            }
        }
    }
}
