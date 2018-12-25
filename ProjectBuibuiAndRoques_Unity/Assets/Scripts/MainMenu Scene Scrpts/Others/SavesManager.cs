using ProjectRoquesAndBuiBui;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Assets.Scripts.Others
{
    public class SavesManager
    {
        static string folder = "Saves\\";
        static string listSaveFiles = "Saves.txt";
        static string key = "";

        public static void SaveGame(Ville v)
        {
            Debug.Log("saving ...");

            if(key == "")
                key = GetKey();
            

            string jsonObj = JsonConvert.SerializeObject(v, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            try
            {
                StreamWriter sw = new StreamWriter("Saves\\" + key + ".txt");
                sw.Write(jsonObj);
                sw.Close();

                sw = new StreamWriter(folder + listSaveFiles, true);
                sw.WriteLine(key + "\\" + DateTime.Now);
                sw.Close();
            }
            catch(IOException ex)
            {
                Debug.Log("Error while saving");
            }
        }

        public static void DeleteSave(string saveKey)
        {
            File.Delete(folder + saveKey + ".txt");
            try
            {
                StreamReader stm = new StreamReader(folder + listSaveFiles);
                string newSavesList = "";
                string line;
                string currentKey = "";
                while((line = stm.ReadLine()) != null)
                {
                    currentKey = line.Split('\\')[0];
                    if(currentKey != saveKey)
                    {
                        newSavesList += line + "\n";
                    }
                }
                stm.Close();

                StreamWriter stmW = new StreamWriter(folder + listSaveFiles, false);
                stmW.WriteLine(newSavesList);
                stmW.Close();
            }
            catch(IOException ex)
            {
                UnityEngine.Object.FindObjectOfType<LogBox>().WriteLog("error while deleting save");
            }
            
        }

        public static List<Save> FetchSaves()
        {
            Debug.Log("Fetching");
            List<Save> saves = new List<Save>();

            StreamReader sr;

            try
            {
                sr = new StreamReader(folder + listSaveFiles);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        string key;
                        DateTime date;

                        try
                        {
                            key = line.Split('\\')[0];

                            date = DateTime.Parse(line.Split('\\')[1]);
                            saves.Add(new Save(key, date));
                        }
                        catch (Exception e)
                        {
                            UnityEngine.Object.FindObjectOfType<LogBox>().WriteLog("corrupt data in save file");

                        }
                    }
                    
                }
                sr.Close();
            }
            catch (IOException ex)
            {
                UnityEngine.Object.FindObjectOfType<LogBox>().WriteLog("error reading saves file");
            }

            UnityEngine.Object.FindObjectOfType<LogBox>().WriteLog("Chargement des sauvegardes reussi");

            return saves;
        }

        public static void LoadGame(string saveKey)
        {
            Ville v;
            JObject vJObject;
            try
            {
                StreamReader stm = new StreamReader(folder + saveKey + ".txt");
                string jsonString = stm.ReadToEnd();
                vJObject = JObject.Parse(jsonString);
                
                v =  JsonConvert.DeserializeObject<Ville>(jsonString);
            }
            catch (Exception e)
            {
                UnityEngine.Object.FindObjectOfType<LogBox>().WriteLog(e.Message);
                return;
            }


            v.LoadData();
            List<Amenagement> newAmenagements = new List<Amenagement>();
            JArray amenagements = (JArray)vJObject["Amenagements"];

            for (int i = 0; i < amenagements.Count; i++)
            {
                JToken a = amenagements[i];
                string type = a["Type"].ToString();
                if(type == "Route")
                {
                    Route r = new Route(a["Nom"].ToString(), Int32.Parse(a["Prix"].ToString()), Int32.Parse(a["Taille"].ToString()), Int32.Parse(a["PosX"].ToString()), Int32.Parse(a["PosY"].ToString()), bool.Parse(a["EstSortie"].ToString()));
                    r.Rotation = new Vector3(float.Parse(a["Rotation"]["x"].ToString()), float.Parse(a["Rotation"]["y"].ToString()), float.Parse(a["Rotation"]["z"].ToString()));
                    newAmenagements.Add(r);
                }
               /* if (type == "Batiment")
                {
                    Batiment b = new Batiment(a["Nom"].ToString(), Int32.Parse(a["Prix"].ToString()), Int32.Parse(a["Taille"].ToString()), Int32.Parse(a["PosX"].ToString()), Int32.Parse(a["PosY"].ToString()), bool.Parse(a["EstSortie"].ToString()));
                    b.Rotation = new Vector3(float.Parse(a["Rotation"]["x"].ToString()), float.Parse(a["Rotation"]["y"].ToString()), float.Parse(a["Rotation"]["z"].ToString()));
                    newAmenagements.Add(b);
                } */
                if (type == "BatimentAdministratif")
                {
                    BatimentAdministratif t = new BatimentAdministratif(Int32.Parse(a["NombreHabitantNecessaire"].ToString()), Int32.Parse(a["CoutMensuel"].ToString()), a["Nom"].ToString(), Int32.Parse(a["Prix"].ToString()), Int32.Parse(a["Taille"].ToString()), Int32.Parse(a["Niveau"].ToString()));
                    t.Rotation = new Vector3(float.Parse(a["Rotation"]["x"].ToString()), float.Parse(a["Rotation"]["y"].ToString()), float.Parse(a["Rotation"]["z"].ToString()));
                    t.PosX = Int32.Parse(a["PosX"].ToString());
                    t.PosY = Int32.Parse(a["PosY"].ToString());
                    newAmenagements.Add(t);
                }
                if (type == "Bureau")
                {
                    Bureau t = new Bureau(Int32.Parse(a["MaxPlaceDispo"].ToString()), float.Parse(a["PrixLocation"].ToString()), Int32.Parse(a["CoutMensuel"].ToString()), a["Nom"].ToString(), Int32.Parse(a["Prix"].ToString()), Int32.Parse(a["Taille"].ToString()), Int32.Parse(a["Niveau"].ToString()));
                    t.Rotation = new Vector3(float.Parse(a["Rotation"]["x"].ToString()), float.Parse(a["Rotation"]["y"].ToString()), float.Parse(a["Rotation"]["z"].ToString()));
                    t.PosX = Int32.Parse(a["PosX"].ToString());
                    t.PosY = Int32.Parse(a["PosY"].ToString());
                    newAmenagements.Add(t);
                }
                if (type == "Commercant")
                {
                    Commercant t = new Commercant(Int32.Parse(a["NbrEmployeMaxAise"].ToString()), Int32.Parse(a["NbrEmployeMaxMoyenne"].ToString()), Int32.Parse(a["NbrEmployeMaxOuvriere"].ToString()), Int32.Parse(a["CoutMensuel"].ToString()), a["Nom"].ToString(), Int32.Parse(a["Prix"].ToString()), Int32.Parse(a["Taille"].ToString()), Int32.Parse(a["Niveau"].ToString()));
                    t.Rotation = new Vector3(float.Parse(a["Rotation"]["x"].ToString()), float.Parse(a["Rotation"]["y"].ToString()), float.Parse(a["Rotation"]["z"].ToString()));
                    t.PosX = Int32.Parse(a["PosX"].ToString());
                    t.PosY = Int32.Parse(a["PosY"].ToString());
                    newAmenagements.Add(t);
                }
                if (type == "CompagnieEau")
                {
                    CompagnieEau t = new CompagnieEau(Int32.Parse(a["ProductionMax"].ToString()), Int32.Parse(a["NbrEmployeMaxAise"].ToString()), Int32.Parse(a["NbrEmployeMaxMoyenne"].ToString()), Int32.Parse(a["NbrEmployeMaxOuvriere"].ToString()), Int32.Parse(a["CoutMensuel"].ToString()), a["Nom"].ToString(), Int32.Parse(a["Prix"].ToString()), Int32.Parse(a["Taille"].ToString()), Int32.Parse(a["Niveau"].ToString()));
                    t.Rotation = new Vector3(float.Parse(a["Rotation"]["x"].ToString()), float.Parse(a["Rotation"]["y"].ToString()), float.Parse(a["Rotation"]["z"].ToString()));
                    t.PosX = Int32.Parse(a["PosX"].ToString());
                    t.PosY = Int32.Parse(a["PosY"].ToString());
                    newAmenagements.Add(t);
                }
                if (type == "CompagnieElectricite")
                {
                    CompagnieElectricite t = new CompagnieElectricite(Int32.Parse(a["ProductionMax"].ToString()), Int32.Parse(a["NbrEmployeMaxAise"].ToString()), Int32.Parse(a["NbrEmployeMaxMoyenne"].ToString()), Int32.Parse(a["NbrEmployeMaxOuvriere"].ToString()), Int32.Parse(a["CoutMensuel"].ToString()), a["Nom"].ToString(), Int32.Parse(a["Prix"].ToString()), Int32.Parse(a["Taille"].ToString()), Int32.Parse(a["Niveau"].ToString()));
                    t.Rotation = new Vector3(float.Parse(a["Rotation"]["x"].ToString()), float.Parse(a["Rotation"]["y"].ToString()), float.Parse(a["Rotation"]["z"].ToString()));
                    t.PosX = Int32.Parse(a["PosX"].ToString());
                    t.PosY = Int32.Parse(a["PosY"].ToString());
                    newAmenagements.Add(t);
                }
                if (type == "CompagnieTransport")
                {
                    CompagnieTransport t = new CompagnieTransport(Int32.Parse(a["CoutMensuel"].ToString()), a["Nom"].ToString(), Int32.Parse(a["Prix"].ToString()), Int32.Parse(a["Taille"].ToString()), Int32.Parse(a["Niveau"].ToString()));
                    t.Rotation = new Vector3(float.Parse(a["Rotation"]["x"].ToString()), float.Parse(a["Rotation"]["y"].ToString()), float.Parse(a["Rotation"]["z"].ToString()));
                    t.PosX = Int32.Parse(a["PosX"].ToString());
                    t.PosY = Int32.Parse(a["PosY"].ToString());
                    newAmenagements.Add(t);
                }
                if (type == "Culture")
                {
                    Culture t = new Culture(Int32.Parse(a["NiveauCulture"].ToString()), Int32.Parse(a["CoutMensuel"].ToString()), a["Nom"].ToString(), Int32.Parse(a["Prix"].ToString()), Int32.Parse(a["Taille"].ToString()), Int32.Parse(a["Niveau"].ToString()));
                    t.Rotation = new Vector3(float.Parse(a["Rotation"]["x"].ToString()), float.Parse(a["Rotation"]["y"].ToString()), float.Parse(a["Rotation"]["z"].ToString()));
                    t.PosX = Int32.Parse(a["PosX"].ToString());
                    t.PosY = Int32.Parse(a["PosY"].ToString());
                    newAmenagements.Add(t);
                }
                /*if (type == "Entreprise")
                {
                    Entreprise t = new Entreprise(Int32.Parse(a["NiveauCulture"].ToString()), Int32.Parse(a["CoutMensuel"].ToString()), a["Nom"].ToString(), Int32.Parse(a["Prix"].ToString()), Int32.Parse(a["Taille"].ToString()), Int32.Parse(a["Niveau"].ToString()));
                    t.Rotation = new Vector3(float.Parse(a["Rotation"]["x"].ToString()), float.Parse(a["Rotation"]["y"].ToString()), float.Parse(a["Rotation"]["z"].ToString()));
                    t.PosX = Int32.Parse(a["PosX"].ToString());
                    t.PosY = Int32.Parse(a["PosY"].ToString());
                    newAmenagements.Add(t);
                }*/
                if (type == "Logement")
                {
                    Logement t = new Logement(Int32.Parse(a["CapaciteMax"].ToString()), (ClasseSocial)Enum.Parse(typeof(ClasseSocial), a["Classe"].ToString()), float.Parse(a["NivBonheur"].ToString()), Int32.Parse(a["CoutMensuel"].ToString()), a["Nom"].ToString(), Int32.Parse(a["Prix"].ToString()), Int32.Parse(a["Taille"].ToString()), Int32.Parse(a["Niveau"].ToString()));
                    t.Rotation = new Vector3(float.Parse(a["Rotation"]["x"].ToString()), float.Parse(a["Rotation"]["y"].ToString()), float.Parse(a["Rotation"]["z"].ToString()));
                    t.PosX = Int32.Parse(a["PosX"].ToString());
                    t.PosY = Int32.Parse(a["PosY"].ToString());
                    newAmenagements.Add(t);
                }
                if (type == "Tourisme")
                {
                    Tourisme t = new Tourisme(Int32.Parse(a["ImpactTourisme"].ToString()), Int32.Parse(a["CoutMensuel"].ToString()), a["Nom"].ToString(), Int32.Parse(a["Prix"].ToString()), Int32.Parse(a["Taille"].ToString()), Int32.Parse(a["Niveau"].ToString()));
                    t.Rotation = new Vector3(float.Parse(a["Rotation"]["x"].ToString()), float.Parse(a["Rotation"]["y"].ToString()), float.Parse(a["Rotation"]["z"].ToString()));
                    t.PosX = Int32.Parse(a["PosX"].ToString());
                    t.PosY = Int32.Parse(a["PosY"].ToString());
                    newAmenagements.Add(t);
                }
                if (type == "Usine")
                {
                    Usine t = new Usine(Int32.Parse(a["NbrEmployeMaxAise"].ToString()), Int32.Parse(a["NbrEmployeMaxMoyenne"].ToString()), Int32.Parse(a["NbrEmployeMaxOuvriere"].ToString()), Int32.Parse(a["CoutMensuel"].ToString()), a["Nom"].ToString(), Int32.Parse(a["Prix"].ToString()), Int32.Parse(a["Taille"].ToString()), Int32.Parse(a["Niveau"].ToString()));
                    t.Rotation = new Vector3(float.Parse(a["Rotation"]["x"].ToString()), float.Parse(a["Rotation"]["y"].ToString()), float.Parse(a["Rotation"]["z"].ToString()));
                    t.PosX = Int32.Parse(a["PosX"].ToString());
                    t.PosY = Int32.Parse(a["PosY"].ToString());
                    newAmenagements.Add(t);
                }
            }

            v.Amenagements = newAmenagements;
            PopulateMap(v);
            UnityEngine.Object.FindObjectOfType<Manager>().StartGame();
            UnityEngine.Object.FindObjectOfType<GameManager>().LoadGame(v);
        }

        static void PopulateMap(Ville v)
        {

            foreach(Amenagement a in v.Amenagements)
            {
                GameObject go = UnityEngine.Object.Instantiate(UnityEngine.Object.FindObjectOfType<BuildingInventory>().GetPrefebBuilding(a.Nom));
                if (a.Taille % 2 == 0)
                {
                    go.transform.position = new Vector3(a.PosX * 10 + (a.Taille - 1) * 5f + 5f, 0.1f, a.PosY * 10 + (a.Taille - 1) * 5f + 5f);
                }
                else
                {
                    go.transform.position = new Vector3(a.PosX * 10 + Mathf.Max(0, a.Taille - 2) * 10f + 5f, 0.1f, a.PosY * 10 + Mathf.Max(0, a.Taille - 2) * 10f + 5f);
                }
                go.GetComponent<AmenagementPrefab>().Amenagement = a;
                go.transform.rotation.eulerAngles.Set(a.Rotation.x, a.Rotation.y, a.Rotation.z);
            }
        }

        static string GetKey()
        {
            //32 - 125 sans 46
            string key = "";
            for (int i = 0; i < 15; i++)
            {

                int rdm;
                do
                {
                    rdm = UnityEngine.Random.Range(97, 123);
                }
                while (rdm == 47 || rdm == 92);
                key += (char)rdm;
            }

            return key;
        }

    }

    public class Save
    {
        string key;
        DateTime saveDate;

        public Save(string key, DateTime date)
        {
            this.key = key;
            saveDate = date;
        }

        public string Key
        {
            get { return key; }
        }

        public DateTime SaveDate
        {
            get { return saveDate; }
        }
    }
}
