using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class Catalogue
    {
        List<Amenagement> catalogues;
        public Catalogue()
        {
            catalogues = new List<Amenagement>();
            /*
            catalogues.Add(new CompagnieEau(35000, 50, 150, 550, 15000, "Central d'eau", 1000000, 2, ConsoleColor.Blue));
            catalogues.Add(new CompagnieElectricite(17000000, 50, 220, 250, 0, "Central nucléaire", 1000000, 3, ConsoleColor.Yellow));
            catalogues.Add(new CompagnieTransport(1, 200, 2, 0, 10000, "Bus RATP", 1000000, 3, ConsoleColor.Green));
            catalogues.Add(new BatimentAdministratif(1000, 2000, "Mairie", 500000, 2, ConsoleColor.White));
            catalogues.Add(new Commercant(100, 250, 500, 0, "Super U", 700000, 3, ConsoleColor.DarkCyan));
            catalogues.Add(new Bureau(20, 5, 2000, 10000, "Bureau 2000", 1000000, 2, ConsoleColor.Gray));
            catalogues.Add(new Logement(2500, ClasseSocial.moyenne, 0, 20000, "Pavillon", 2000000, 3, ConsoleColor.DarkMagenta));
            catalogues.Add(new Logement(600, ClasseSocial.aisee, 0, 40000, "Villa", 5000000, 2, ConsoleColor.DarkYellow));
            catalogues.Add(new Logement(3000, ClasseSocial.ouvriere, 0, 20000, "Villa", 5000000, 2, ConsoleColor.DarkYellow));
            catalogues.Add(new Primaire(500,100,50,6000, 10000, "Ferme", 3000000, 4, ConsoleColor.DarkRed));
            catalogues.Add(new Tourisme(8, 10000, "ardin Babylonne", 1000000, 1, ConsoleColor.DarkGreen));
            catalogues.Add(new Culture(10, 18000, "Ecole", 750000, 2, ConsoleColor.DarkRed));
            catalogues.Add(new Usine(80, 300, 1000, 0, "Usine", 5000000, 3, ConsoleColor.DarkBlue));
            catalogues.Add(new Route("Route", 10, 1, ConsoleColor.DarkGray, 0, 0, false));
            */
        }

        internal List<Amenagement> Catalogues
        {
            get
            {
                return catalogues;
            }

            set
            {
                catalogues = value;
            }
        }

        public void Affichage()
        {
            foreach(Amenagement a in catalogues)
            {
                Console.WriteLine(a.ToString());
            }
        }

        public string Listing()
        {
            int compteur = 1;
            string liste = "";
            foreach(Amenagement a in catalogues)
            {
                if (liste != "")
                    liste += "\n";
                liste += compteur+ ". " + a.Nom;
                compteur++;
            }
            return liste;
        }
    }
}
