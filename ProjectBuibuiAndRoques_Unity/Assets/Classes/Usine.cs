using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class Usine : Entreprise
    {
        public Usine(int NbrEmployeMaxAise, int NbrEmployeMaxMoyenne, int NbrEmployeMaxOuvriere, int coutMensuel, string nom, int prix, int taille, int niveau) : base(0.2f, 1.1f, NbrEmployeMaxAise, NbrEmployeMaxMoyenne, NbrEmployeMaxOuvriere, coutMensuel, nom, prix, taille, niveau)
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
        public override string AffichageAchat()
        {
            return base.AffichageAchat();
        }
    }
}
