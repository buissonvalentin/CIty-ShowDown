using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class Player
    {
        string pseudo;
        bool isInMatchMaking;
        bool isInGame;
        bool isReady;

        public Player(string pseudo)
        {
            this.pseudo = pseudo;
            isInGame = false;
            isInMatchMaking = false;
        }

        public string Pseudo
        {
            get { return pseudo; }
        }

        public bool IsInMatchMaking
        {
            get { return isInMatchMaking; }
            set { isInMatchMaking = value; }
        }

        public bool IsInGame
        {
            get { return isInGame; }
            set { isInGame = value; }
        }

        public bool IsReady
        {
            get { return IsReady; }
            set { isReady = value; }
        }

    }
}
