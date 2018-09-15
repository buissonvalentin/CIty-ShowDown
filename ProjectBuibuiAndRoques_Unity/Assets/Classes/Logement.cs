using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    public class Logement : Secondaire
    {
        int capaciteMax;
        ClasseSocial classe;
        float nivBonheur;
        int minOccupation;//En dessous de cette valeur, l'état paye des charges pour l'entretien
        int occupationActuelle;
        public Logement(int capaciteMax, ClasseSocial classe, float nivBonheur, int coutMensuel, string nom, int prix, int taille, int niveau) : base(coutMensuel, nom, prix, taille, niveau)
        {
            this.capaciteMax = capaciteMax;
            this.classe = classe;
            this.nivBonheur = nivBonheur;
            this.minOccupation = capaciteMax / 4;
            this.occupationActuelle = 0;
        }

        public int MinOccupation
        {
            get
            {
                return minOccupation;
            }

            set
            {
                minOccupation = value;
            }
        }

        public int OccupationActuelle
        {
            get
            {
                return occupationActuelle;
            }

            set
            {
                occupationActuelle = value;
            }
        }

        public float NivBonheur
        {
            get
            {
                return nivBonheur;
            }

            set
            {
                nivBonheur = value;
            }
        }

        public int CapaciteMax
        {
            get
            {
                return capaciteMax;
            }

            set
            {
                capaciteMax = value;
            }
        }

        public ClasseSocial Classe
        {
            get
            {
                return classe;
            }

            set
            {
                classe = value;
            }
        }

        public override string ToString()
        {
            return base.ToString()+"\nCapacité Max logement : "+capaciteMax+"\nClasse sociale du logement : "+Convert.ToString(classe)+"\nNiveau de bonheur du logement : "+nivBonheur+"\nMinimum Occupation : "+minOccupation;
        }

        public override string AffichageAchat()
        {
            return base.AffichageAchat() +"\nClasse : " + Convert.ToString(classe) + "\nCapacité : " + capaciteMax + " habt";
        }
    }

    public enum ClasseSocial { aisee, moyenne, ouvriere, tous}
}
