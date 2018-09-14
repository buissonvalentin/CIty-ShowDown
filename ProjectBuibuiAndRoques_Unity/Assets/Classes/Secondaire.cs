using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    public abstract class Secondaire : Batiment
    {
        public Secondaire(int coutMensuel, string nom, int prix, int taille, int niveau) : base(coutMensuel, nom, prix, taille, niveau)
        { }
        public Secondaire(float coefAttractivite, float coefCulture, int coutMensuel, string nom, int prix, int taille, int niveau) : base(coefAttractivite, coefCulture, coutMensuel, nom, prix, taille, niveau)
        { }
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
