using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class Game
    {
        public DateTime begin;
        int id;
        List<Info> infos;
        string playerFound;

        public Game(int id)
        {
            this.id = id;
            infos = new List<Info>();
        }

        public void ResetInfoList()
        {
            infos = new List<Info>();
        }

        public void AddInfo(Info i)
        {
            foreach(Info inf in infos)
            {
                if(inf.Id == i.Id)
                {
                    return;
                }
            }
            infos.Add(i);
        }

        public void AddInfo(int id,string type, string message)
        {
            AddInfo(new Info(id, type, message));
        }

        public int Id
        {
            get { return id; }
        }

        public List<Info> Infos
        {
            get { return infos; }
        }

        public string PlayerFound
        {
            get
            {
                return playerFound;
            }

            set
            {
                playerFound = value;
            }
        }
    }
}
