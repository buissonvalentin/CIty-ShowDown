using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class CompagnieTransport : Secondaire, IParametrable
    {
        int nombreTransport;
        int capaciteTransport;
        float prixTransport;
        int frequentation;

        public int NombreTransport
        {
            get
            {
                return nombreTransport;
            }

            set
            {
                nombreTransport = value;
            }
        }

        public int CapaciteTransport
        {
            get
            {
                return capaciteTransport;
            }

            set
            {
                capaciteTransport = value;
            }
        }

        public float PrixTransport
        {
            get
            {
                return prixTransport;
            }

            set
            {
                prixTransport = value;
            }
        }

        public int Frequentation
        {
            get
            {
                return frequentation;
            }

            set
            {
                frequentation = value;
            }
        }

        public CompagnieTransport( int coutMensuel, string nom, int prix, int taille, int niveau) : base(0.4f, 1.1f, coutMensuel, nom, prix, taille, niveau)
        {
            this.nombreTransport = 0;
            this.capaciteTransport = 0;
            this.prixTransport = 0;
            this.frequentation = 0;
        }



        public override string ToString()
        {
            return base.ToString()+"\nNombre de Transport : "+nombreTransport+"\nCapacité des transports : "+capaciteTransport+"\nPrix du ticket : "+prixTransport+"\nFréquentation : "+frequentation;
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
            int cpt = 0;
            Console.WriteLine("Prix ticket : ");
            AfficheSlideBar(1, prixTransport, 5, 0.5);
            Console.WriteLine("Nombre de transport : ");
            AfficheSlideBar(0, nombreTransport, 100, 10);

            while (continuer)
            {
                /*
                Console.SetCursorPosition(0, cpt * 2 + 1);
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    cpt -= 1;
                    if (cpt < 0) cpt = 0;
                }
                if (key == ConsoleKey.DownArrow)
                {
                    cpt += 1;
                    if (cpt > 2) cpt = 2;
                }
                if (key == ConsoleKey.RightArrow)
                {
                    ChangeCoefImpot(cpt, 1);
                }
                if (key == ConsoleKey.LeftArrow)
                {
                    ChangeCoefImpot(cpt, -1);
                }
                if (key == ConsoleKey.Escape)
                {
                    continuer = false;
                }

                Console.Clear();
                Console.WriteLine("Prix ticket : ");
                AfficheSlideBar(1, prixTransport, 5, 0.5);
                Console.WriteLine("Nombre de transport : ");
                AfficheSlideBar(0, nombreTransport, 100, 10);
                */
            }
            

        }

        void ChangeCoefImpot(int cpt, double coef)
        {
            if (cpt == 0) prixTransport += 0.5f * (float)coef;
            if (prixTransport > 5) prixTransport = 5;
            if (prixTransport < 1) prixTransport = 1;
            if (cpt == 1) nombreTransport += (int)(10 * coef);
            if (nombreTransport > 100) nombreTransport = 100;
            if (nombreTransport < 0) nombreTransport = 0;
        }
        
    }
}
