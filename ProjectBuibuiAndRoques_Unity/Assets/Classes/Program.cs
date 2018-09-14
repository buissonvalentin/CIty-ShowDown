using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjectRoquesAndBuiBui
{
    class Program
    {
        
         

        static void Main(string[] args)
        {
            Catalogue test = new Catalogue();
            Ville sebastopol = new Ville(5000000, 50, 50, 20,1.5f,0.2f,3,1600);
            Thread gestion = new Thread(sebastopol.EvolutionVariablesParTour);

            sebastopol.Map.Carte[5, 5] = new Route("sortie", 0, 1, 0, 0, true);


            gestion.Start();
            while (true)
            {
            
            }
            
        }

        
    }
}
