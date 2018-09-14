using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    public abstract class Batiment : Amenagement
    {
        float coefBonheur;
        float coefAttractivite;//1 est le maximum et 0 est le minimum
        float coefCulture;//1 est le maximum et 0 est le minimum
        int coutMensuel;

        public Batiment(int coutMensuel,string nom, int prix, int taille, int niveau): base(nom,prix,taille, niveau)
        {
            this.coefBonheur = 1.1f;
            this.coefAttractivite = 1.1f;
            this.coefCulture = 1.1f;
            this.coutMensuel = coutMensuel;
        }
        public Batiment(float coefAttractivite, float coefCulture, int coutMensuel, string nom, int prix, int taille, int niveau) : base(nom, prix, taille, niveau)
        {
            this.coefBonheur = 1.1f;
            this.coefAttractivite = coefAttractivite;
            this.coefCulture = coefCulture;
            this.coutMensuel = coutMensuel;
        }


        public override string ToString()
        {
            return base.ToString() + "\nCoefficient Attractivité : " + coefAttractivite + "\nCoût mensuel : " + coutMensuel;
        }
        public override string AffichageAchat()
        {
            return "Coût mensuel : " + coutMensuel;
        }

       

        public int CoutMensuel
        {
            get
            {
                return coutMensuel;
            }

            set
            {
                coutMensuel = value;
            }
        }

        public float CoefCulture
        {
            get
            {
                return coefCulture;
            }

            set
            {
                coefCulture = value;
            }
        }

        public float CoefAttractivite
        {
            get
            {
                return coefAttractivite;
            }

            set
            {
                coefAttractivite = value;
            }
        }

        public float CoefBonheur
        {
            get
            {
                return coefBonheur;
            }

            set
            {
                coefBonheur = value;
            }
        }

        
    }
}
