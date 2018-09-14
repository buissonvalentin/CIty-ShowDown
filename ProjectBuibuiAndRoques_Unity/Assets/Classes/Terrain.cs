using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    public class Terrain
    {
        Amenagement[,] carte;
        int taille;

        public Terrain(int taille)
        {
            this.taille = taille;
            carte = new Amenagement[taille, taille];
        }

        public int Taille
        {
            get { return taille; }
        }

        public Amenagement[,] Carte
        {
            get { return carte; }
        }

        public void PlacerUnBatiment(Amenagement a, Ville v)
        {   
            PoserAmenagement(a);

            if (a is Logement)
            {
                v.AjoutLogement((a as Logement));
            }
            if (a is Route)
            {
                VerifierConnectionToutLesBatiments(v.Amenagements);
            }
            else
            {
                VerifierConnectionBatiment(a);
            }  
        }

        public bool VerifierPlace(Amenagement a)
        {
            if (a.PosY + a.Taille > taille || a.PosX + a.Taille > taille || a.PosX < 0 || a.PosY < 0) return false;
            

            for (int i = a.PosY; i < a.PosY + a.Taille; i++)
            {
                for (int j = a.PosX; j < a.PosX + a.Taille; j++)
                {
                    if (carte[i, j] != null) return false;
                }
            }
            return true;
        }

        public Amenagement RecupererAmenagement(int z, int x)
        {
            if (z < 0 || z >= taille || x < 0 || x >= taille) return null;
            return carte[z, x];
        }

        void PoserAmenagement(Amenagement a)
        {
            for (int i = a.PosY; i < a.PosY + a.Taille; i++)
            {
                for (int j = a.PosX; j < a.PosX + a.Taille; j++)
                {
                    carte[i, j] = a;
                }
            }
        }

        public void SupprimerBatiment(Amenagement a)
        {
            
            if (a != null)
            {
                int id = a.IdAmenagement;

                for (int i = Math.Max(0, a.PosY - a.Taille); i < Math.Min(taille, a.PosY + a.Taille); i++)
                {
                    for (int j = Math.Max(0, a.PosX - a.Taille); j < Math.Min(taille, a.PosX + a.Taille); j++)
                    {
                        if (carte[i, j] != null && carte[i, j].IdAmenagement == id) carte[i, j] = null;
                    }
                }
            }
            
        }

        void VerifierConnectionBatiment(Amenagement a)
        {
            /*
            int gauche = Math.Max(a.PosX - 1, 0);
            int droite = Math.Min(a.PosX + a.Taille, taille - 1);
            int haut = Math.Max(a.PosY - 1, 0);
            int bas = Math.Min(a.PosY + a.Taille, taille - 1);
            for (int i = gauche; i <= droite; i++)
            {
                for(int j = haut; j <= bas; j++)
                {
                    if(!(j == a.PosX - 1 && i == a.PosY + a.Taille) && !(j == a.PosX - 1 && i == a.PosY - 1) && !(j == a.PosX + a.Taille && i == a.PosY + a.Taille) && !(j == a.PosX + a.Taille && i == a.PosY - 1))                  
                    {
                        if(carte[i, j] != null && carte[i, j] is Route)
                        {
                            Amenagement t = carte[i, j];
                            bool connecte = PathFinding(new List<Route>(), new Queue<Route> ( new[]{(t as Route) }));
                            if (connecte)
                            {
                                (a as Batiment).EstConnecte = true;
                                return;
                            }
                            
                        }
                    }
                }
            }

            (a as Batiment).EstConnecte = false;

            */

            if(a.PosX > 0)
            {
                for(int i = a.PosY; i < a.PosY + a.Taille; i++)
                {
                    if(carte[i, a.PosX - 1] != null && carte[i, a.PosX - 1] is Route)
                    {
                        Amenagement t = carte[i, a.PosX - 1];
                        bool connecte = PathFinding(new List<Route>(), new Queue<Route>(new[] { (t as Route) }));
                        if (connecte)
                        {
                            (a as Batiment).EstConnecte = true;
                            return;
                        }
                    }
                }
            }
            if (a.PosX + a.Taille < taille - 1)
            {
                for (int i = a.PosY ; i < a.PosY + a.Taille; i++)
                {
                    if (carte[i, a.PosX + a.Taille] != null && carte[i, a.PosX + a.Taille] is Route)
                    {
                        Amenagement t = carte[i, a.PosX + a.Taille];
                        bool connecte = PathFinding(new List<Route>(), new Queue<Route>(new[] { (t as Route) }));
                        if (connecte)
                        {
                            (a as Batiment).EstConnecte = true;
                            return;
                        }
                    }
                }
            }
            if (a.PosY > 0)
            {
                for (int i = a.PosX ; i < a.PosX + a.Taille; i++)
                {
                    if (carte[a.PosY - 1, i] != null && carte[a.PosY - 1, i] is Route)
                    {
                        Amenagement t = carte[a.PosY - 1, i];
                        bool connecte = PathFinding(new List<Route>(), new Queue<Route>(new[] { (t as Route) }));
                        if (connecte)
                        {
                            (a as Batiment).EstConnecte = true;
                            return;
                        }
                    }
                }
            }
            if (a.PosY + a.Taille< taille - 1)
            {
                for (int i = a.PosX ; i < a.PosX + a.Taille; i++)
                {
                    if (carte[a.PosY + a.Taille, i] != null && carte[a.PosY + a.Taille, i] is Route)
                    {
                        Amenagement t = carte[a.PosY + a.Taille, i];
                        bool connecte = PathFinding(new List<Route>(), new Queue<Route>(new[] { (t as Route) }));
                        if (connecte)
                        {
                            (a as Batiment).EstConnecte = true;
                            return;
                        }
                    }
                }
            }
            (a as Batiment).EstConnecte = false;
        }

        void VerifierConnectionToutLesBatiments(List<Amenagement> amenagements) {
            foreach(Amenagement a in amenagements)
            {
                if((a is Batiment))
                {
                    VerifierConnectionBatiment(a); 
                }
            }
        }

        bool PathFinding(List<Route> routesbloquees, Queue<Route> routesachercher)
        {
            Route actuel = routesachercher.Dequeue();

            if (actuel.EstSortie) return true;

            foreach (Route r in AjoutRoutesAdjacentes(routesbloquees, actuel))
            {
                routesachercher.Enqueue(r);
            }
            routesbloquees.Add(actuel);

            if (routesachercher.Count == 0) return false;
            else return PathFinding(routesbloquees, routesachercher);
        }

        List<Route> AjoutRoutesAdjacentes(List<Route> routesbloquees, Route r)
        {
            List<Route> temp = new List<Route>();
            if (r.PosX > 0 && carte[r.PosY, r.PosX - 1] is Route && !routesbloquees.Contains(carte[r.PosY, r.PosX - 1] as Route)) temp.Add(carte[r.PosY, r.PosX - 1] as Route);
            if (r.PosY > 0 && carte[r.PosY - 1, r.PosX] is Route && !routesbloquees.Contains(carte[r.PosY - 1, r.PosX] as Route)) temp.Add(carte[r.PosY - 1, r.PosX] as Route);
            if (r.PosX < taille - 1 && carte[r.PosY, r.PosX + 1] is Route && !routesbloquees.Contains(carte[r.PosY, r.PosX + 1] as Route)) temp.Add(carte[r.PosY, r.PosX + 1] as Route);
            if (r.PosY < taille - 1 && carte[r.PosY + 1, r.PosX] is Route && !routesbloquees.Contains(carte[r.PosY + 1, r.PosX] as Route)) temp.Add(carte[r.PosY + 1, r.PosX] as Route);

            return temp;
        }

    }
}
