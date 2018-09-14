using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class BatimentAdministratif : Tertiaire
    {
        private int nombreHabitantNecessaire;

        public BatimentAdministratif(int nombreHabitantNecessaire, int coutMensuel, string nom, int prix, int taille, int niveau) : base(0.8f, 0.9f, coutMensuel, nom, prix, taille, niveau)
        {
            this.nombreHabitantNecessaire = nombreHabitantNecessaire;
        }
    }
}
