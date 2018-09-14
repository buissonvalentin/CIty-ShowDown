using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    public class Legislation
    {
        string nom;
        string description;
        bool active;
        int prixLoi;
        int prixAnnulation;
        int coutMensuel;
        Loi positive;
        Loi negative;

        public Legislation(string nom, string description, bool active, int prixLoi, int coutMensuel, Loi effetPositif, Loi effetnegatif)
        {
            this.nom = nom;
            this.Description = description;
            this.active = active;
            this.prixLoi = prixLoi;
            this.prixAnnulation = prixLoi * 3;
            this.coutMensuel = coutMensuel;
            this.positive = effetPositif;
            this.negative = effetnegatif;
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

        public int PrixLoi
        {
            get
            {
                return prixLoi;
            }

            set
            {
                prixLoi = value;
            }
        }

        public int PrixAnnulation
        {
            get
            {
                return prixAnnulation;
            }

            set
            {
                prixAnnulation = value;
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

        internal Loi Positive
        {
            get
            {
                return positive;
            }

            set
            {
                positive = value;
            }
        }

        internal Loi Negative
        {
            get
            {
                return negative;
            }

            set
            {
                negative = value;
            }
        }
    }
}
