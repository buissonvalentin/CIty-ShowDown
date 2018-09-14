using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    delegate void EffetBonus();

    public class Bonus
    {
        string nom;
        int dureeBonus;
        string description;
        bool active;
        int coutBonus;

        public Bonus(string nom, int dureeBonus,string description, int coutBonus)
        {
            this.nom = nom;
            this.dureeBonus = dureeBonus;
            active = false;
            this.description = description;
            this.coutBonus = coutBonus;
        }

        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;
            }
        }

        public int DureeBonus
        {
            get
            {
                return dureeBonus;
            }

            set
            {
                dureeBonus = value;
            }
        }

        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        public int CoutBonus
        {
            get
            {
                return coutBonus;
            }

        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }
    }
}
