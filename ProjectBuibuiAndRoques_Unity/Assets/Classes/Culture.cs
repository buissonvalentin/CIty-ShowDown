using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class Culture :Tertiaire
    {
        int niveauCulture;
        public Culture(int niveauCulture, int coutMensuel, string nom, int prix, int taille, int niveau) : base(1f, 0.6f, coutMensuel, nom, prix, taille, niveau)
        {
            this.niveauCulture = niveauCulture;
        }

        public int NiveauCulture
        {
            get
            {
                return niveauCulture;
            }

            set
            {
                niveauCulture = value;
            }
        }

        public override string ToString()
        {
            return base.ToString()+"\nNiveau culture : "+niveauCulture;
        }
        public override string AffichageAchat()
        {
            return base.AffichageAchat()+ "\nNiveau culture : " + niveauCulture;
        }
    }
}
