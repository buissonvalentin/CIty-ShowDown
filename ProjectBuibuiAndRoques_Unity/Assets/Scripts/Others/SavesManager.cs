using ProjectRoquesAndBuiBui;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

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
                Debug.Log("error while deleting save");
            }
            
        }

        public static List<Save> FetchSaves()
        {
            List<Save> saves = new List<Save>();

            StreamReader sr;

            try
            {
                sr = new StreamReader(folder + listSaveFiles);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string key;
                    DateTime date;

                    try
                    {
                        key = line.Split('\\')[0];
                        Debug.Log(key);
                        date = DateTime.Parse(line.Split('\\')[1]);
                        Debug.Log(date);
                        saves.Add(new Save(key, date));
                    }
                    catch (Exception e)
                    {
                        Debug.Log("corrupt data in save file");
                    }
                }
            }
            catch (IOException ex)
            {
                Debug.Log("error reading saves file");
            }

            return saves;
        }

        public static void LoadGame(string saveKey)
        {
            Ville v;
            try
            {
                StreamReader stm = new StreamReader(folder + saveKey + ".txt");
                string jsonString = stm.ReadToEnd();
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                v =  JsonConvert.DeserializeObject<Ville>(jsonString, settings);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return;
            }


            v.LoadData();
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
