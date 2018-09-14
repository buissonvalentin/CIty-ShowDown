using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class LoiEconomique : Loi
    {

        ClasseSocial impacter;
        public LoiEconomique(float coef, ClasseSocial impacter) : base(coef)
        {
            this.impacter = impacter;
        }

        /// <summary>
        /// Renvoie le cout par mensualité
        /// </summary>
        /// <param name="population">Structure du tableau {Population entière, Aisée, Moyenne, Ouvrière}</param>
        /// <returns></returns>
        public float CoutLoi(int[] population)
        {
            float cout = 0;
            if (impacter == ClasseSocial.tous)
                cout = population[0] * Coef;
            if (impacter == ClasseSocial.aisee)
                cout = population[1] * Coef;
            if (impacter == ClasseSocial.moyenne)
                cout = population[2] * Coef;
            if (impacter == ClasseSocial.ouvriere)
                cout = population[3] * Coef;
            return cout;
        }
    }
}
