using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class Route : Amenagement, IComparable<Route>
    {
        bool estSortie;
        public Route(string nom, int prix, int taille, int x, int y, bool estSortie) : this(nom, prix, taille, estSortie)
        {
            PosX = x;
            PosY = y;
        }

        public Route(string nom, int prix, int taille, bool estSortie) : base(nom, prix, taille, 0)
        {
            this.estSortie = estSortie;
        }

        public int CompareTo(Route other)
        {
            return IdAmenagement.CompareTo(other.IdAmenagement);
        }

        public bool EstSortie
        {
            get { return estSortie; }
        }
    }
}
