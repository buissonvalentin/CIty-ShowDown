using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class Tourisme : Tertiaire
    {
        private int impactTourisme;
        public Tourisme(int impactTourisme, int coutMensuel, string nom, int prix, int taille, int niveau) : base(1, 1, coutMensuel, nom, prix, taille, niveau)
        {
            this.impactTourisme = impactTourisme;
        }

        public int ImpactTourisme
        {
            get
            {
                return impactTourisme;
            }

            set
            {
                impactTourisme = value;
            }
        }

        public override string ToString()
        {
            return base.ToString()+"\nImpact tourisme : "+impactTourisme;
        }
        public override string AffichageAchat()
        {
            return base.AffichageAchat() + "\nImpact tourisme : " + impactTourisme;
        }
    }
}
