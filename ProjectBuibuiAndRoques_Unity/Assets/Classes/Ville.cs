using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjectRoquesAndBuiBui
{
    public class Ville
    {
        private float argent;
        private float revenu;
        private int population;
        private int populationAisee;
        private int populationMoyenne;
        private int populationOuvriere;
        private int personneChomage;
        int eauRestante;
        int energieRestante;
        int nourritureRestante;
        private int personneChomageAisee;
        private int personneChomageMoyenne;
        private int personneChomageOuvriere;
        private int capaciteLogementAisee;
        private int capaciteLogementMoyenne;
        private int capaciteLogementOuvriere;
        private float eauConsommeParPersonne;
        private float energieConsommeParPersonne;
        private float attractivite;//entre 0 et 100
        private float culture;//entre 0 et 100
        private float bonheur;//entre 0 et 100
        private float bonheurAisee;//entre 0 et 100
        private float bonheurOuvriere;//entre 0 et 100
        private float bonheurMoyenne;//entre 0 et 100   
        private float coefImpotAisee;
        private float coefImpotMoyenne;
        private float coefImpotOuvriere;
        private float coefImpotEntreprise;
        private float impotParCycle;
        private float depenseParCycle;
        private float energieConsomme;
        private float eauConsomme;
        private int nourritureConsomme;
        private float energieProduite;
        private float eauProduite;
        private float nourritureProduite;
        private int capaciteLogement;
        private int quotaPointBonus;
        private Marche marcheFinancier;
        private List<Amenagement> amenagements;
        private List<Bonus> bonus;
        private List<Legislation> loisExistante;
        private Terrain map;
        int tempsRestant;
        bool jeuEnPause;

        public Ville(float argent, float attractivite, float bonheur, int taille, float valEau, float valElectricite, float eauConsommeParPersonne, float energieConsommeParPersonne)
        {
            this.argent = argent;
            this.eauConsommeParPersonne = eauConsommeParPersonne;
            this.energieConsommeParPersonne = energieConsommeParPersonne;
            this.population = 0;
            this.capaciteLogement = 0;
            this.attractivite = attractivite;
            this.bonheur = 50;
            this.bonheurAisee = 50;
            this.bonheurMoyenne = 50;
            this.bonheurOuvriere = 50;
            this.coefImpotAisee = 1;
            this.coefImpotMoyenne = 1;
            this.coefImpotOuvriere = 1;
            this.coefImpotEntreprise = 1;
            this.populationAisee = 0;
            this.populationMoyenne = 0;
            this.populationOuvriere = 0;
            this.revenu = 0;
            this.culture = 0;
            this.impotParCycle = 0;
            this.depenseParCycle = 0;
            amenagements = new List<Amenagement>();
            bonus = new List<Bonus>();
            map = new Terrain(taille);
            loisExistante = new List<Legislation>();
            marcheFinancier = new Marche(valElectricite, valEau);
            CreationListeBonus();
            CreationListeLoi();
            jeuEnPause = true;
            this.quotaPointBonus = 10;
        }

        public override string ToString()
        {
            return "Argent : " + argent + "\nRevenu : " + revenu + "\nPopulation : " + population + "\nPopulation aisée : " + populationAisee + "\nPopulation moyenne : " + populationMoyenne + "\nPopulation ouvrière : " + populationOuvriere + "\nCapacité Logement : " + capaciteLogement + "\nCulture : " + culture + "\nAttractivité : " + attractivite + "\nEau Consomme : " + eauConsomme + "\nEnergie Consomme : " + energieConsomme + "\nBonheur : " + bonheur + "\nPersonne chômage : " + personneChomage + "\nNourriture : " + nourritureConsomme + "\nCoef Impot Aisée : " + coefImpotAisee;
        }

        public Terrain Map
        {
            get { return map; }
        }

        public List<Amenagement> Amenagements { get { return amenagements; } }

        bool PeutAjouterAmenagement(Amenagement a)
        {
            if (a.Prix <= argent)
            {
                amenagements.Add(a);
                argent -= a.Prix;
                return true;
            }
            else
            {
                //Console.SetCursorPosition(25, 0);
                Console.WriteLine("Vous n'avez pas assez d'argent");
                return false;
            }
        }

        public void SupprimerAmenagement(Amenagement a)
        {
            if (a is Logement)
            {
                RetirerLogement((a as Logement));
            }
            map.SupprimerBatiment(a);
            amenagements.Remove(a);
        }

        public bool PlacerUnAmenagement(Amenagement a)
        {
            bool temp;
            if (temp = PeutAjouterAmenagement(a))
            {
                map.PlacerUnBatiment(a, this);
            }
            return temp;
        }

        private void CreationListeLoi()
        {
            loisExistante.Add(new Legislation("Tris des déchets", "cette lois fait des choses", false, 50000, 1000, new LoiTourisme(1.1f), new LoiEconomique(0.24f, ClasseSocial.tous)));
        }

        private void CreationListeBonus()
        {
            bonus.Add(new Bonus("The Show", 8,"Organisation d'un évènement mondialement connu",2));
            bonus.Add(new Bonus("Réforme de l'école", 8, "Intensification des programmes scolaires",1));
            bonus.Add(new Bonus("Aide de l'UE", 8, "Les déficits sont absorbés par l'UE",1));
            bonus.Add(new Bonus("Aide de l'UE 2", 2, "L'UE débloque de l'argent pour vous aider à faire vos réformes",2));
            bonus.Add(new Bonus("Les ressources ne sont plus un problème", 8, "L'UE vous fournit les ressources nécessaires à la survie de votre ville",2));
        }

        public void ActiverLegislation(string nomLoi)
        {
            foreach (Legislation l in loisExistante)
            {
                if (l.Nom == nomLoi && !l.Active)
                {
                    l.Active = true;
                    argent -= l.PrixLoi;
                }
            }
        }

        public void DesactiverLegislation(string nomLoi)
        {
            foreach (Legislation l in loisExistante)
            {
                if (l.Nom == nomLoi && l.Active)
                {
                    l.Active = false;
                    argent -= l.PrixAnnulation;
                }
            }
        }

        public bool ActiverBonus(string nomBonus)
        {
            bool result = false;
            foreach(Bonus b in bonus)
            {
                if(b.Nom==nomBonus && !b.Active)
                {
                    if (quotaPointBonus >= b.CoutBonus)
                    {
                        result = true;
                        b.Active = true;
                        quotaPointBonus -= b.CoutBonus;
                    }
                }
            }

            return result;
        }

        #region Méthode thread

        public void EvolutionVariablesParTour()
        {
            while (true)
            {
                if (!jeuEnPause)
                {
                    CalculBureau();
                    CalculRessourcesProduite();
                    CalculerPopulation();
                    CalculChomage();
                    CalculRessourcesConsomme();
                    CalculCulture();
                    CalculAttractivite();
                    CalculRevenu();
                    argent += revenu;
                    CalculBonheur();
                    Thread.Sleep(30000);
                }
                else
                {
                    Thread.Sleep(100);
                }

            }
        }

        #region Calcul Revenu par tour

        public void CalculRevenu()
        {
            int nombreCommercant = NombreCommercant();
            revenu = 0;
            foreach (Amenagement a in amenagements)
            {
                revenu -= CoutEntretien(a);
                revenu -= CoutProduction(a);
                if (a is Batiment && (a as Batiment).EstConnecte)
                {
                    revenu += ImpotEntreprise(a);
                    if (a is Commercant)
                        revenu += revenuCommercant((a as Commercant), nombreCommercant);
                }


            }
            revenu -= CoutMensuelLoi();
            revenu += VenteProduit();
            revenu += ImpotParticulier();
            int[] tab = { population, populationAisee, populationMoyenne, populationOuvriere };
            foreach (Legislation a in loisExistante)
            {
                if (a.Active)
                {
                    if (a.Negative is LoiEconomique)
                        revenu -= (a.Negative as LoiEconomique).CoutLoi(tab);
                }
            }
            if(bonus[2].Active)
            {
                if (revenu < 0)
                    revenu = 0;
                VerificationDureeBonus(2);
            }
            if(bonus[3].Active)
            {
                revenu += 1200000;
                VerificationDureeBonus(3);
            }

        }

        private int NombreCommercant()
        {
            int nombreC = 0;
            foreach (Amenagement a in amenagements)
            {
                if (a is Commercant)
                {
                    nombreC += (a as Commercant).Niveau;
                }
            }
            return nombreC;
        }

        private float revenuCommercant(Commercant commerce, int nombreCommercant)
        {
            float constante = Math.Min(population / nombreCommercant, 200 * commerce.Niveau) * (float)bonheur / 100;//Nombre d'acheteur
            float multiplicateur = 0;
            if (commerce.PrixVente <= commerce.PrixOptimal)
                multiplicateur = 30;
            else
                multiplicateur = -(30 / 9100) * commerce.PrixVente + 300000 / 9100;
            float revenu = constante * 30 * multiplicateur;
            return revenu;
        }

        private int CoutEntretien(Amenagement batisse)
        {
            int revenu = 0;
            if (batisse is Bureau)
            {
                if ((batisse as Bureau).MinOccupation > (batisse as Bureau).PlacesOccupees)
                    revenu += (batisse as Bureau).CoutMensuel * ((batisse as Bureau).MinOccupation - (batisse as Bureau).PlacesOccupees);

            }
            else if (batisse is Logement)
            {
                if ((batisse as Logement).MinOccupation > (batisse as Logement).OccupationActuelle)
                    revenu += (batisse as Logement).CoutMensuel;
            }
            else
            {
                if (!(batisse is Route))
                    revenu += (batisse as Batiment).CoutMensuel;
            }
            return revenu;
        }

        private float CoutProduction(Amenagement batisse)
        {
            float depense = 0;
            if (batisse is CompagnieEau)
            {
                depense += (batisse as CompagnieEau).EauProduite * 1.5f;
            }
            else if (batisse is CompagnieElectricite)
                depense += (batisse as CompagnieElectricite).EnergieProduite * 0.10f;
            else if (batisse is CompagnieTransport)
                depense += (batisse as CompagnieTransport).NombreTransport * 200;
            else if (batisse is Primaire)
                depense += (batisse as Primaire).NourritureProduite * 520;
            return depense;
        }

        private float CoutMensuelLoi()
        {
            float depense = 0;
            foreach (Legislation a in loisExistante)
            {
                if (a.Active)
                    depense += a.CoutMensuel;
            }
            return depense;
        }

        private float CalculRevenu(Amenagement batisse)
        {
            //(batisse as Commercant).ProduitVendu = (batisse as Commercant).PrixOptimal / Math.Sqrt(1);//Trouver équation produit vendu
            float recette = 0;
            if (batisse is Bureau)
                recette += (batisse as Bureau).PlacesOccupees * (float)(batisse as Bureau).PrixLocation;
            else if (batisse is Usine)
                recette += 15000 * (float)(batisse as Usine).CoefOccupation;
            else if (batisse is Commercant)
                recette += (float)(batisse as Commercant).ProduitVendu * (batisse as Commercant).PrixVente * 0.2f;
            else if (batisse is CompagnieTransport)
                recette += (batisse as CompagnieTransport).PrixTransport * (batisse as CompagnieTransport).Frequentation;
            return recette;
        }

        private float ImpotEntreprise(Amenagement batisse)
        {
            float recette = 0;
            if (batisse is Bureau)
                recette += (batisse as Bureau).PlacesOccupees * 1500 * (float)coefImpotEntreprise;
            if (batisse is Usine)
                recette += ((batisse as Usine).NbrEmployeActuelOuvriere + (batisse as Usine).NbrEmployeActuelMoyenne + (batisse as Usine).NbrEmployeActuelAise) * 100;
            if (batisse is Commercant)
                recette += ((batisse as Commercant).NbrEmployeActuelAise + (batisse as Commercant).NbrEmployeActuelMoyenne + (batisse as Commercant).NbrEmployeActuelOuvriere) * 100;
            return recette;
        }

        private float ImpotParticulier()
        {
            float recette = 0;
            recette += (populationAisee - personneChomageAisee) * 850 * coefImpotAisee;
            recette += (populationMoyenne - personneChomageMoyenne) * 350 * coefImpotMoyenne;
            recette += (populationOuvriere - personneChomageOuvriere) * 150 * coefImpotOuvriere;
            return recette;
        }

        
        private float VenteProduit()
        {
            float recette = 0;
            //Vente énergie
            recette += (energieProduite - energieRestante) * marcheFinancier.Electricite;
            energieRestante = 0;
            //Vente Eau
            recette += (eauProduite - eauRestante) * marcheFinancier.Eau;
            eauRestante = 0;
            //Vente Nourriture
            recette += (nourritureProduite - nourritureRestante) * 570;
            nourritureRestante = 0;
            return recette;

        }
        #endregion

        private void CalculUtilisationProduction()
        {
            eauRestante = Convert.ToInt32(eauConsomme);
            energieRestante = Convert.ToInt32(energieConsomme);
            nourritureRestante = nourritureConsomme;
            foreach (Amenagement a in amenagements)
            {
                if (a is CompagnieEau)
                    eauRestante = (a as CompagnieEau).EauUtilisee(eauRestante);
                if (a is CompagnieElectricite)
                    energieRestante = (a as CompagnieElectricite).EnergieUtilisee(energieRestante);
                if (a is Primaire)
                    nourritureRestante = (a as Primaire).NourritureUtilisee(nourritureRestante);
            }

        }

        private void CalculChomage()
        {
            int nombreEmployeAisee = 0;
            int nombreEmployeeMoyenne = 0;
            int nombreEmployeeOuvriere = 0;
            int populationAiseeRestante = populationAisee;
            int populationMoyenneRestante = populationMoyenne;
            int populationOuvriereRestante = populationOuvriere;
            foreach (Amenagement a in amenagements)
            {
                if (a is IEmployable)
                {



                    if (populationAiseeRestante > (a as IEmployable).NbrEmployeMaxAise)
                    {
                        (a as IEmployable).NbrEmployeActuelAise = (a as IEmployable).NbrEmployeMaxAise;
                        populationAiseeRestante -= (a as IEmployable).NbrEmployeActuelAise;
                        (a as IEmployable).CoefOccupation = 1 * (a as IEmployable).NbrEmployeMaxAise / ((a as IEmployable).NbrEmployeMaxAise + (a as IEmployable).NbrEmployeMaxMoyenne + (a as IEmployable).NbrEmployeMaxOuvriere);//Occupation de 1 * la proportion d'employés aisée

                    }
                    else if (populationAiseeRestante > 0)
                    {
                        (a as IEmployable).NbrEmployeActuelAise = populationAiseeRestante;
                        populationAiseeRestante = 0;
                        (a as IEmployable).CoefOccupation = ((a as IEmployable).NbrEmployeActuelAise / (a as IEmployable).NbrEmployeMaxAise) * (a as IEmployable).NbrEmployeMaxAise / ((a as IEmployable).NbrEmployeMaxAise + (a as IEmployable).NbrEmployeMaxMoyenne + (a as IEmployable).NbrEmployeMaxOuvriere);
                    }
                    else
                    {
                        (a as IEmployable).CoefOccupation = 0;
                        (a as IEmployable).NbrEmployeActuelAise = 0;
                    }
                    if (populationMoyenneRestante > (a as IEmployable).NbrEmployeMaxMoyenne)
                    {
                        (a as IEmployable).NbrEmployeActuelMoyenne = (a as IEmployable).NbrEmployeMaxMoyenne;
                        populationMoyenneRestante -= (a as IEmployable).NbrEmployeActuelMoyenne;
                        (a as IEmployable).CoefOccupation += 1 * (a as IEmployable).NbrEmployeMaxMoyenne / ((a as IEmployable).NbrEmployeMaxAise + (a as IEmployable).NbrEmployeMaxMoyenne + (a as IEmployable).NbrEmployeMaxOuvriere);

                    }
                    else if (populationMoyenneRestante > 0)
                    {
                        (a as IEmployable).NbrEmployeActuelMoyenne = populationMoyenneRestante;
                        populationMoyenneRestante = 0;
                        (a as IEmployable).CoefOccupation += ((a as IEmployable).NbrEmployeActuelMoyenne / (a as IEmployable).NbrEmployeMaxAise) * (a as IEmployable).NbrEmployeMaxAise / ((a as IEmployable).NbrEmployeMaxAise + (a as IEmployable).NbrEmployeMaxMoyenne + (a as IEmployable).NbrEmployeMaxOuvriere);
                    }
                    else
                    {
                        (a as IEmployable).NbrEmployeActuelMoyenne = 0;
                    }
                    if (populationOuvriereRestante > (a as IEmployable).NbrEmployeMaxOuvriere)
                    {
                        (a as IEmployable).NbrEmployeActuelOuvriere = (a as IEmployable).NbrEmployeMaxOuvriere;
                        populationOuvriereRestante -= (a as IEmployable).NbrEmployeActuelOuvriere;
                        (a as IEmployable).CoefOccupation += 1 * (a as IEmployable).NbrEmployeActuelOuvriere / ((a as IEmployable).NbrEmployeMaxAise + (a as IEmployable).NbrEmployeMaxMoyenne + (a as IEmployable).NbrEmployeMaxOuvriere);

                    }
                    else if (populationOuvriereRestante > 0)
                    {
                        (a as IEmployable).NbrEmployeActuelOuvriere = populationOuvriereRestante;
                        populationOuvriereRestante = 0;
                        (a as IEmployable).CoefOccupation += ((a as IEmployable).NbrEmployeActuelOuvriere / (a as IEmployable).NbrEmployeMaxAise) * (a as IEmployable).NbrEmployeMaxAise / ((a as IEmployable).NbrEmployeMaxAise + (a as IEmployable).NbrEmployeMaxMoyenne + (a as IEmployable).NbrEmployeMaxOuvriere);
                    }
                    else
                    {
                        (a as IEmployable).NbrEmployeActuelOuvriere = 0;
                    }

                    nombreEmployeAisee += (a as IEmployable).NbrEmployeActuelAise;
                    nombreEmployeeMoyenne += (a as IEmployable).NbrEmployeActuelMoyenne;
                    nombreEmployeeOuvriere += (a as IEmployable).NbrEmployeActuelOuvriere;

                }

            }
            personneChomageAisee = populationAisee - nombreEmployeAisee;
            personneChomageMoyenne = populationMoyenne - nombreEmployeeMoyenne;
            personneChomageOuvriere = populationOuvriere - nombreEmployeeOuvriere;
            personneChomage = personneChomageAisee + personneChomageMoyenne + personneChomageOuvriere;
        }

        public void CalculRessourcesConsomme()
        {
            nourritureConsomme = population;
            energieConsomme = population * energieConsommeParPersonne;
            eauConsomme = population * eauConsommeParPersonne;
        }

        public void CalculRessourcesProduite()
        {
            energieProduite = 0;
            eauProduite = 0;
            nourritureProduite = 0;

            foreach (Amenagement a in amenagements)
            {
                if (a is CompagnieEau && (a as Entreprise).estFonctionnel())
                {
                    eauProduite += (a as CompagnieEau).EauProduite;
                }
                if (a is CompagnieElectricite && (a as Entreprise).estFonctionnel())
                {
                    energieProduite += (a as CompagnieElectricite).EnergieProduite;
                }
                if (a is Primaire && (a as Primaire).estFonctionnel())
                {
                    nourritureProduite += (a as Primaire).NourritureProduite;
                }
            }
        }

        private void CalculBonheur()
        {
            if (population > 0)
            {
                bonheur = 0;
                //Bonheur en fonction de la nourriture
                if (nourritureConsomme > population || bonus[4].Active)//La population ne veut pas gâcher
                    bonheur += 20;
                else if (nourritureConsomme < population / 2)
                    bonheur += 0;
                else
                    bonheur += 40 * (float)nourritureConsomme / Math.Max((float)population, 1) - 20;
                //Bonheur en fonction de l'eau
                if (eauProduite > eauConsomme || bonus[4].Active)//La population ne veut pas gâcher
                    bonheur += 15;
                else if (eauProduite < eauConsomme / 2)
                    bonheur += 0;
                else
                    bonheur += 30 * (eauProduite / Math.Max(eauConsomme, 1)) - 15;
                //Bonheur en fonction de l'electricité
                if (energieProduite > energieConsomme || bonus[4].Active)//La population ne veut pas gâcher
                    bonheur += 10;
                else if (energieProduite == 0)
                    bonheur += 0;
                else
                    bonheur += 10 * (energieProduite / Math.Max(energieConsomme, 1));
                //Bonheur en fonction la culture
                VerificationDureeBonus(4);
                bonheur += culture * 0.15f;
                //Bonheur en fonction la culture
                bonheur += attractivite * 0.05f;
                foreach (Legislation a in loisExistante)
                {
                    if (a.Active)
                    {
                        if (a.Negative is LoiSociale)
                        {
                            bonheur = bonheur * a.Negative.Coef;//Si la loi a un effet bénéfique
                        }
                        if (a.Positive is LoiSociale)
                            bonheur = bonheur * a.Positive.Coef;
                    }
                }
                CalculBonheurParClasse();
            }
            else
            {
                bonheur = 50;
                bonheurAisee = 50;
                bonheurMoyenne = 50;
                bonheurOuvriere = 50;
            }

        }

        private void CalculBonheurParClasse()
        {
            if (coefImpotAisee != 1)
            {
                int test = 0;
            }
            bonheurAisee = bonheur + 15 * (1 - (coefImpotAisee / 100));

            bonheurMoyenne = bonheur + 15 * (float)(1 - Math.Pow((coefImpotMoyenne / 100), 2));
            bonheurOuvriere = bonheur + 15 * (float)(1 - Math.Pow(coefImpotOuvriere / 100, 3));
            if (capaciteLogementAisee > populationAisee)
                bonheurAisee += 20;
            else if (capaciteLogementAisee < populationAisee * 0.90)
                bonheurAisee += 0;
            else if (capaciteLogementAisee < populationAisee * 0.5)
                bonheurAisee = bonheurAisee * 0.5f;
            else
                bonheurAisee += 200 * ((float)capaciteLogementAisee / Math.Max((float)populationAisee, 1)) - 180;
            if (capaciteLogementMoyenne > populationMoyenne)
                bonheurMoyenne += 20;
            else if (capaciteLogementMoyenne < populationMoyenne * 0.85)
                bonheurMoyenne += 0;
            else if (capaciteLogementMoyenne < populationMoyenne * 0.5)
                bonheurMoyenne = bonheurMoyenne * 0.5f;
            else
                bonheurMoyenne += 133.33f * ((float)capaciteLogementMoyenne / (float)Math.Max(populationMoyenne, 1)) - 113.33f;
            if (capaciteLogementOuvriere > populationOuvriere)
                bonheurOuvriere += 20;
            else if (capaciteLogementOuvriere < populationOuvriere * 0.8)
                bonheurOuvriere += 0;
            else if (capaciteLogementOuvriere < populationOuvriere * 0.5)
                bonheurOuvriere = bonheurOuvriere * 0.5f;
            else
                bonheurOuvriere += 100 * ((float)capaciteLogementOuvriere / (float)Math.Max(1, populationOuvriere)) - 80;
            if (populationAisee != 0)
                bonheurAisee = bonheurAisee * (1.15f - ((float)personneChomageAisee / (float)Math.Max(1, populationAisee)));
            if (populationMoyenne != 0)
                bonheurMoyenne = bonheurMoyenne * (1.15f - ((float)personneChomageMoyenne / (float)Math.Max(populationMoyenne, 1)));
            if (populationOuvriere != 0)
                bonheurOuvriere = bonheurOuvriere * (1.15f - ((float)personneChomageOuvriere / (float)Math.Max(populationOuvriere, 1)));
            if (coefImpotAisee > 20)
                bonheurAisee = bonheurAisee * 0.6f;
            if (coefImpotMoyenne > 25)
                bonheurMoyenne = bonheurMoyenne * 0.6f;
            if (coefImpotOuvriere > 30)
                bonheurOuvriere = bonheurOuvriere * 0.6f;
            if (bonheurAisee > 100)
                bonheurAisee = 100;
            if (bonheurMoyenne > 100)
                bonheurMoyenne = 100;
            if (bonheurOuvriere > 100)
                bonheurOuvriere = 100;
            float proportionPopAisee = (float)populationAisee / (float)(population + 1);
            float proportionPopMoyenne = (float)populationMoyenne / (float)(population + 1);
            float proportionPopOuvriere = 1 - proportionPopAisee - proportionPopMoyenne;
            CoefNegatifBonheur();
            if (bonheurAisee < 0)
                bonheurAisee = 0;
            if (bonheurMoyenne < 0)
                bonheurMoyenne = 0;
            if (bonheurOuvriere < 0)
                bonheurOuvriere = 0;
            bonheur = proportionPopAisee * bonheurAisee + proportionPopMoyenne * bonheurMoyenne + proportionPopOuvriere * bonheurOuvriere;
        }

        private void CoefNegatifBonheur()
        {
            if (eauProduite < eauConsomme / 2 || nourritureProduite < nourritureProduite / 2)
            {
                bonheurAisee = 0;
                bonheurMoyenne = 0;
                bonheurOuvriere = 0;
            }
            else if (energieProduite < energieConsomme / 2 || culture < 30)
            {
                bonheurAisee = bonheurAisee * 0.6f;
                bonheurMoyenne = bonheurMoyenne * 0.6f;
                bonheurOuvriere = bonheurOuvriere * 0.6f;
            }

        }

        private void CalculerPopulation()
        {
            int nombreArrivee = 0;
            int capaciteLogementRestant = capaciteLogement - population;
            if (capaciteLogementRestant < 0)
                capaciteLogementRestant = 0;
            if (bonheur >= 50)
            {
                if (population < 100)
                {
                    nombreArrivee = Math.Min(capaciteLogementRestant + 1, 50);
                    populationAisee += Convert.ToInt32(nombreArrivee * 0.2);
                    populationOuvriere += Convert.ToInt32(nombreArrivee * 0.3);
                    populationMoyenne += Convert.ToInt32(nombreArrivee * 0.5);
                }
                else if (population < 1000)
                {
                    nombreArrivee = Math.Min(capaciteLogementRestant + 2, 50 + Convert.ToInt32(population * 0.3));
                    populationAisee += Convert.ToInt32(nombreArrivee * 0.2);
                    populationMoyenne += Convert.ToInt32(nombreArrivee * 0.5);
                    populationOuvriere += Convert.ToInt32(nombreArrivee * 0.3);
                }
                else if (population < 10000)
                {
                    nombreArrivee = Math.Min(capaciteLogementRestant + 3, Convert.ToInt32(population * 0.2));
                    populationAisee += Convert.ToInt32(nombreArrivee * 0.2);
                    populationMoyenne += Convert.ToInt32(nombreArrivee * 0.5);
                    populationOuvriere += Convert.ToInt32(nombreArrivee * 0.3);
                }
                else if (population < 50000)
                {
                    nombreArrivee = Math.Min(capaciteLogementRestant + 4, Convert.ToInt32(population * 0.08));
                    populationAisee += Convert.ToInt32(nombreArrivee * 0.2);
                    populationMoyenne += Convert.ToInt32(nombreArrivee * 0.5);
                    populationOuvriere += Convert.ToInt32(nombreArrivee * 0.3);
                }
                else if (population < 100000)
                {
                    nombreArrivee = Math.Min(capaciteLogementRestant + 5, Convert.ToInt32(population * 0.05));
                    populationAisee += Convert.ToInt32(nombreArrivee * 0.2);
                    populationMoyenne += Convert.ToInt32(nombreArrivee * 0.5);
                    populationOuvriere += Convert.ToInt32(nombreArrivee * 0.3);
                }
                else
                {
                    nombreArrivee = Math.Min(capaciteLogementRestant + 6, Convert.ToInt32(population * 0.02));
                    populationAisee += Convert.ToInt32(nombreArrivee * 0.2);
                    populationMoyenne += Convert.ToInt32(nombreArrivee * 0.5);
                    populationOuvriere += Convert.ToInt32(nombreArrivee * 0.3);
                }

            }
            if (bonheur > 70)
            {
                if (population < 100)
                {
                    nombreArrivee = Math.Min(capaciteLogementRestant + 1, 100);
                    populationAisee += Convert.ToInt32(nombreArrivee * 0.2);
                    populationOuvriere += Convert.ToInt32(nombreArrivee * 0.3);
                    populationMoyenne += Convert.ToInt32(nombreArrivee * 0.5);
                }
                else if (population < 1000)
                {
                    nombreArrivee = Math.Min(capaciteLogementRestant + 2, 100 + Convert.ToInt32(population * 0.4));
                    populationAisee += Convert.ToInt32(nombreArrivee * 0.2);
                    populationMoyenne += Convert.ToInt32(nombreArrivee * 0.5);
                    populationOuvriere += Convert.ToInt32(nombreArrivee * 0.3);
                }
                else if (population < 10000)
                {
                    nombreArrivee = Math.Min(capaciteLogementRestant + 3, Convert.ToInt32(population * 0.3));
                    populationAisee += Convert.ToInt32(nombreArrivee * 0.2);
                    populationMoyenne += Convert.ToInt32(nombreArrivee * 0.5);
                    populationOuvriere += Convert.ToInt32(nombreArrivee * 0.3);
                }
                else if (population < 50000)
                {
                    nombreArrivee = Math.Min(capaciteLogementRestant + 4, Convert.ToInt32(population * 0.2));
                    populationAisee += Convert.ToInt32(nombreArrivee * 0.2);
                    populationMoyenne += Convert.ToInt32(nombreArrivee * 0.5);
                    populationOuvriere += Convert.ToInt32(nombreArrivee * 0.3);
                }
                else if (population < 100000)
                {
                    nombreArrivee = Math.Min(capaciteLogementRestant + 5, Convert.ToInt32(population * 0.1));
                    populationAisee += Convert.ToInt32(nombreArrivee * 0.2);
                    populationMoyenne += Convert.ToInt32(nombreArrivee * 0.5);
                    populationOuvriere += Convert.ToInt32(nombreArrivee * 0.3);
                }
                else
                {
                    nombreArrivee = Math.Min(capaciteLogementRestant + 6, Convert.ToInt32(population * 0.05));
                    populationAisee += Convert.ToInt32(nombreArrivee * 0.2);
                    populationMoyenne += Convert.ToInt32(nombreArrivee * 0.5);
                    populationOuvriere += Convert.ToInt32(nombreArrivee * 0.3);
                }

            }
            else if (bonheur < 50 && bonheur > 30)
            {

                populationAisee = populationAisee * 18 / 19;
                populationMoyenne = populationMoyenne * 24 / 25;
                populationOuvriere = populationOuvriere * 24 / 25;

            }
            else if (bonheur < 30)
            {

                populationAisee = populationAisee * 14 / 16;
                populationMoyenne = populationMoyenne * 15 / 16;
                populationOuvriere = populationOuvriere * 15 / 16;

            }
            population = populationAisee + populationMoyenne + populationOuvriere;
            CalculPopulationParClasse();
        }

        private void CalculPopulationParClasse()
        {
            if (bonheurAisee < 50 && bonheurAisee > 40)
                populationAisee = populationAisee * 13 / 16;
            else if (bonheurAisee < 40)
                populationAisee = populationAisee * 3 / 4;
            else if (bonheurAisee > 80)
                populationAisee = populationAisee * 23 / 21;
            if (bonheurMoyenne < 50 && bonheurMoyenne > 30)
                populationMoyenne = populationMoyenne * 7 / 8;
            else if (bonheurMoyenne < 30)
                populationMoyenne = populationMoyenne * 5 / 7;
            else if (bonheurMoyenne > 70)
                populationMoyenne = populationMoyenne * 8 / 7;
            if (bonheurOuvriere < 40 && bonheurOuvriere > 20)
                populationOuvriere = populationOuvriere * 7 / 8;
            else if (bonheurOuvriere < 20)
                populationOuvriere = populationOuvriere * 3 / 4;
            else if (bonheurOuvriere > 70)
                populationOuvriere = populationOuvriere * 9 / 7;
            population = populationOuvriere + populationMoyenne + populationAisee;
        }

        private void CalculAttractivite()
        {
            float sommeMultiplicateur = 0;
            float coefMultiplicateur = 0;
            attractivite = 0;//Repars de 0
            int compteur = 0;
            int niveauTourisme = 0;
            foreach (Amenagement a in amenagements)
            {
                if (a is Batiment)
                {
                    if ((a as Batiment).CoefAttractivite != 1.1)//Vérifie que le bâtiment à un impact sur l'attractivité
                    {
                        sommeMultiplicateur += (a as Batiment).CoefAttractivite;//Somme les coefficients d'attractivité
                        compteur++;
                    }
                    if (a is Tourisme)
                    {
                        niveauTourisme += (a as Tourisme).ImpactTourisme;//Somme l'impact des monuments sur le tourisme
                    }
                }
            }
            if (compteur != 0)
                coefMultiplicateur = sommeMultiplicateur / compteur;//Récupère le coef d'attractivité de la ville en elle-même (chiffre compris entre 0 et 1)
            attractivite = 50 * coefMultiplicateur;//Représente la moitié du pourcentage d'attractivité

            //On considère qu'une ville est très attractive lorsqu'elle a un niveau de tourisme pour 500 habitants
            float inter = ((population + 200) / 200);//On récupère le nombre de paquets de 500 habitants que l'on a et on a rajouté 500 pour les cas de début de partie
            if (inter < niveauTourisme)//S'il y a plus d'un niveau de tourisme pour 500 habitants
                attractivite += 50;//La ville est très attractive de ceux point de vue
            else if (inter != 0)
                attractivite += 50 * (niveauTourisme / inter);
            foreach (Legislation a in loisExistante)
            {
                if (a.Active)
                {
                    if (a.Negative is LoiTourisme)
                    {
                        attractivite = a.Negative.Coef * attractivite;
                    }
                    if (a.Positive is LoiTourisme)
                        attractivite = a.Positive.Coef * attractivite;
                }
            }
            if(bonus[0].Active)
            {
                attractivite = attractivite * 1.2f;
                VerificationDureeBonus(0);
            }

        }

        /// <summary>
        /// Diminue la durée de bonus et supprime le bonus si le temps est terminé
        /// </summary>
        /// <param name="positionBonus"></param>
        private void VerificationDureeBonus(int positionBonus)
        {
            bonus[positionBonus].DureeBonus--;
            if (bonus[positionBonus].DureeBonus <= 0)
                bonus[positionBonus].Active = false;
        }

        private void CalculBureau()
        {
            float envieArrivee = 0;
            foreach (Amenagement a in amenagements)
            {
                if (a is Bureau)
                {
                    envieArrivee = 1 / 2 * bonheur + 1 / 2 * (100 - 0.1f * (float)Math.Pow(coefImpotEntreprise, 2));
                }
            }
        }

        private void CalculCulture()
        {

            culture = 0;//Repars de 0
            int niveauCulture = 0;
            foreach (Amenagement a in amenagements)
            {
                if (a is Culture)
                {
                    niveauCulture += (a as Culture).NiveauCulture;//Somme l'impact des monuments sur le tourisme
                }
            }
            //On considère qu'une ville est très attractive lorsqu'elle a un niveau de tourisme pour 500 habitants
            float inter = ((population + 500) / 500);//On récupère le nombre de paquets de 500 habitants que l'on a et on a rajouté 500 pour les cas de début de partie
            if (inter < niveauCulture)//S'il y a plus d'un niveau de tourisme pour 500 habitants
                culture = 100;//La ville est très attractive de ceux point de vue
            else if (inter != 0)
                culture = 100 * (niveauCulture / inter);
            if(bonus[1].Active)
            {
                culture = culture * 1.2f;
                VerificationDureeBonus(1);
            }

        }

        public void CalculLogements()
        {
            capaciteLogement = 0;
            capaciteLogementAisee = 0;
            capaciteLogementMoyenne = 0;
            capaciteLogementOuvriere = 0;
            foreach (Amenagement a in amenagements)
            {
                if (a is Logement && (a as Batiment).EstConnecte)
                {
                    Logement l = (Logement)a;
                    if (l.Classe == ClasseSocial.ouvriere)
                        capaciteLogementOuvriere += l.CapaciteMax;
                    else if (l.Classe == ClasseSocial.moyenne)
                        capaciteLogementMoyenne += l.CapaciteMax;
                    else
                        capaciteLogementAisee += l.CapaciteMax;
                }
            }
            capaciteLogement = capaciteLogementAisee + capaciteLogementMoyenne + capaciteLogementOuvriere;
        }
        #endregion

        public void AjoutLogement(Logement l)
        {
            if (l.Classe == ClasseSocial.ouvriere)
                capaciteLogementOuvriere += l.CapaciteMax;
            else if (l.Classe == ClasseSocial.moyenne)
                capaciteLogementMoyenne += l.CapaciteMax;
            else
                capaciteLogementAisee += l.CapaciteMax;

            capaciteLogement += l.CapaciteMax;
        }

        private void RetirerLogement(Logement l)
        {

            if (l.Classe == ClasseSocial.ouvriere)
                capaciteLogementOuvriere -= l.CapaciteMax;
            else if (l.Classe == ClasseSocial.moyenne)
                capaciteLogementMoyenne -= l.CapaciteMax;
            else
                capaciteLogementAisee -= l.CapaciteMax;

            capaciteLogement -= l.CapaciteMax;
        }

        public void Play()
        {
            jeuEnPause = false;
        }

        public void Pause()
        {
            jeuEnPause = true;
        }


        #region Convertion

        private int EssayerConvertirInt(string aConvertir)
        {
            int aRetourner;
            try
            {
                aRetourner = Convert.ToInt32(aConvertir);
            }
            catch
            {
                aRetourner = -1;
            }
            return aRetourner;
        }

        private bool EssayerConvertirBool(string aConvertir)
        {
            bool aRetourner;
            try
            {
                aRetourner = Convert.ToBoolean(aConvertir);
            }
            catch
            {
                aRetourner = false;
            }
            return aRetourner;
        }

        private double EssayerConvertirDouble(string aConvertir)
        {
            double aRetourner;
            try
            {
                aRetourner = Convert.ToDouble(aConvertir);
            }
            catch
            {
                aRetourner = -1;
            }
            return aRetourner;
        }

        #endregion

        #region Proprietes
        public float Attractivite
        {
            get
            {
                return attractivite;
            }

            set
            {
                attractivite = value;
            }
        }

        public float Bonheur
        {
            get
            {
                return bonheur;
            }

            set
            {
                bonheur = value;
            }
        }

        public float Culture
        {
            get
            {
                return culture;
            }

            set
            {
                culture = value;
            }
        }

        public int Population
        {
            get
            {
                return population;
            }

            set
            {
                population = value;
            }
        }

        public float Revenu
        {
            get
            {
                return revenu;
            }

            set
            {
                revenu = value;
            }
        }

        public float Argent
        {
            get
            {
                return argent;
            }

            set
            {
                argent = value;
            }
        }

        public float CoefImpotAisee
        {
            get
            {
                return coefImpotAisee;
            }

            set
            {
                coefImpotAisee = value;
                if (coefImpotAisee < 0) coefImpotAisee = 0;
                if (coefImpotAisee > 100) coefImpotAisee = 100;
            }
        }

        public float CoefImpotMoyenne
        {
            get
            {
                return coefImpotMoyenne;
            }

            set
            {
                coefImpotMoyenne = value;
                if (coefImpotMoyenne < 0) coefImpotMoyenne = 0;
                if (coefImpotMoyenne > 100) coefImpotMoyenne = 100;
            }
        }

        public float CoefImpotOuvriere
        {
            get
            {
                return coefImpotOuvriere;
            }

            set
            {
                coefImpotOuvriere = value;
                if (coefImpotOuvriere < 0) coefImpotOuvriere = 0;
                if (coefImpotOuvriere > 100) coefImpotOuvriere = 100;
            }
        }

        public float CoefImpotEntreprise
        {
            get
            {
                return coefImpotEntreprise;
            }

            set
            {
                coefImpotEntreprise = value;
                if (coefImpotEntreprise < 0) coefImpotEntreprise = 0;
                if (coefImpotEntreprise > 100) coefImpotEntreprise = 100;
            }
        }

        public float BonheurAisee
        {
            get
            {
                return bonheurAisee;
            }

            set
            {
                bonheurAisee = value;
            }
        }

        public float BonheurOuvriere
        {
            get
            {
                return bonheurOuvriere;
            }

            set
            {
                bonheurOuvriere = value;
            }
        }

        public float BonheurMoyenne
        {
            get
            {
                return bonheurMoyenne;
            }

            set
            {
                bonheurMoyenne = value;
            }
        }

        public int PopulationAisee
        {
            get
            {
                return populationAisee;
            }

            set
            {
                populationAisee = value;
            }
        }

        public int PopulationMoyenne
        {
            get
            {
                return populationMoyenne;
            }

            set
            {
                populationMoyenne = value;
            }
        }

        public int PopulationOuvriere
        {
            get
            {
                return populationOuvriere;
            }

            set
            {
                populationOuvriere = value;
            }
        }

        public int PersonneChomageAisee
        {
            get
            {
                return personneChomageAisee;
            }

            set
            {
                personneChomageAisee = value;
            }
        }

        public int PersonneChomageMoyenne
        {
            get
            {
                return personneChomageMoyenne;
            }

            set
            {
                personneChomageMoyenne = value;
            }
        }

        public int PersonneChomageOuvriere
        {
            get
            {
                return personneChomageOuvriere;
            }

            set
            {
                personneChomageOuvriere = value;
            }
        }

        public int CapaciteLogementAisee
        {
            get
            {
                return capaciteLogementAisee;
            }

            set
            {
                capaciteLogementAisee = value;
            }
        }

        public int CapaciteLogementMoyenne
        {
            get
            {
                return capaciteLogementMoyenne;
            }

            set
            {
                capaciteLogementMoyenne = value;
            }
        }

        public int CapaciteLogementOuvriere
        {
            get
            {
                return capaciteLogementOuvriere;
            }

            set
            {
                capaciteLogementOuvriere = value;
            }
        }

        public float EnergieConsomme
        {
            get
            {
                return energieConsomme;
            }

            set
            {
                energieConsomme = value;
            }
        }

        public float EauConsomme
        {
            get
            {
                return eauConsomme;
            }

            set
            {
                eauConsomme = value;
            }
        }

        public int NourritureConsomme
        {
            get
            {
                return nourritureConsomme;
            }

            set
            {
                nourritureConsomme = value;
            }
        }

        public float EnergieProduite
        {
            get
            {
                return energieProduite;
            }

            set
            {
                energieProduite = value;
            }
        }

        public float EauProduite
        {
            get
            {
                return eauProduite;
            }

            set
            {
                eauProduite = value;
            }
        }

        public float NourritureProduite
        {
            get
            {
                return nourritureProduite;
            }

            set
            {
                nourritureProduite = value;
            }
        }

        public int PersonneChomage
        {
            get
            {
                return personneChomage;
            }

            set
            {
                personneChomage = value;
            }
        }

        public int CapaciteLogement
        {
            get
            {
                return capaciteLogement;
            }

            set
            {
                capaciteLogement = value;
            }
        }

        public bool JeuEnPause
        {
            get
            {
                return jeuEnPause;
            }

            set
            {
                jeuEnPause = value;
            }
        }

        public List<Legislation> LoisExistantes
        {
            get { return loisExistante; }
        }

        public List<Bonus> BonusExistant
        {
            get { return bonus; }
        }

        public int QuotaPointBonus
        {
            get
            {
                return quotaPointBonus;
            }
        }

        public int TempsRestant
        {
            get
            {
                return tempsRestant;
            }

            set
            {
                tempsRestant = value;
            }
        }

        #endregion
    }
}
