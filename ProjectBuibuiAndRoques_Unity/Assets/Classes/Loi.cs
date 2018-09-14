using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    public class Loi
    {


        float coef;

        public Loi(float coef)
        {
            this.coef = coef;
        }

        public float Coef
        {
            get
            {
                return coef;
            }

            set
            {
                coef = value;
            }
        }


    }
}
