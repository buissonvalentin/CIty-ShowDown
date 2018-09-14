using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRoquesAndBuiBui
{
    class Info
    {
        string type;
        string message;
        bool read;
        int id;

        public Info(int id, string type, string message)
        {
            this.id = id;
            this.type = type;
            this.message = message;
            read = false;
        }

        public string Type
        {
            get { return type; }
        }

        public string Message
        {
            get { return message; }
        }

        public bool Read
        {
            get { return read; }
            set { read = value; }
        }

        public int Id
        {
            get { return id; }
        }
    }
}
