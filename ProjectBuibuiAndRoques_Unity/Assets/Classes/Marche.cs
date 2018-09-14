using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class Marche
    {
        private float electricite;
        private float eau;
        public Marche(float electricite, float eau)
        {
            this.electricite = electricite;
            this.eau = eau;
        }

        public float Electricite
        {
            get
            {
                return electricite;
            }

            set
            {
                electricite = value;
            }
        }

        public float Eau
        {
            get
            {
                return eau;
            }

            set
            {
                eau = value;
            }
        }
    }
}
