using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    abstract class Tertiaire : Batiment
    {
        public Tertiaire(int coutMensuel, string nom, int prix, int taille, int niveau) : base(coutMensuel, nom, prix, taille, niveau)
        {
        }
        public Tertiaire(float coefCulture, float coefAttractivite, int coutMensuel, string nom, int prix, int taille, int niveau) : base(coefAttractivite, coefCulture, coutMensuel, nom, prix, taille, niveau)
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
