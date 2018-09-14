
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;

namespace ProjectRoquesAndBuiBui
{
   
    class ServerBdd
    {//aaaaaA8*
        SqlConnection connection;
        string connectionstring = "SERVER=db750323406.db.1and1.com;DATABASE=db750323406;UID=dbo750323406;PASSWORD=aaaaaA8*;";
        string connectionstring2 = "SERVER=fdb22.runhosting.com;DATABASE=2805603_bdd;UID=2805603_bdd;PASSWORD=aaaaaA8*;";

        public delegate void dgEventRaiser();
        public event dgEventRaiser OnMatchFound;

        Game game;
        Player player;

        public ServerBdd(Player p)
        {
            player = p;
        }

        public bool Connect()
        {
            connection = new SqlConnection(connectionstring2);

            connection.StateChange += (s, o) =>
            {
                Console.WriteLine("Connection state : " + connection.State);
            };

            try
            {
                connection.Open();
                Console.WriteLine("connected");
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("failed to established connection");
                connection = null;
                return false;
            }
            
        }

        public void Close()
        {
            if(connection != null)
            {
                try
                {
                    connection.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Failed to close connection");
                }
                finally
                {
                    connection = null;
                }
            }
        }

        public void CreatePlayer(string pseudo)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into player (pseudo) values('" + pseudo + "');";
            command.ExecuteNonQuery();
        }

        public void JoinMatchMaking()
        {
            if (!player.IsInMatchMaking)
            {
                player.IsInMatchMaking = true;
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "insert into matchmaking (player, jointime, playermatch) values('" + player.Pseudo + "', NOW(), NULL);";
                command.ExecuteNonQuery();
                LookForTeamMate();
            }
            else
            {
                Console.WriteLine("You are already in matchmaking");
            }

            

        }

        public string GetLatestPlayerInMatchMaking()
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "select player, jointime from matchmaking where player != '" + player.Pseudo + "' order by jointime;";

            SqlDataReader reader = command.ExecuteReader();

            string temp = "";

            if (reader.Read())
            {
                temp = reader.GetString(0);
                
            }

            reader.Close();
            return temp;

        }

        void LookForTeamMate()
        {
            string playerFound = GetLatestPlayerInMatchMaking();
            if (playerFound == "") // il n'y a pas d'autres joueurs en recherche de partie, on attend donc quelqu'un 
            {
                // lorsqu'une personne se connecte elle regarde d'abord si un joueur attend deja
                // si c'est le cas, le joueur en attente est directement ajouté à une nouvelle partie par le joueur qui vient de se connecté
                // on regarde donc toutes les x secondes si nous avons été ajouté a une partie
                Thread matchmakingThread = new Thread(() =>
                {
                    bool isLooking = true;
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "select id, pseudoplayer1 from game where pseudoplayer2 = '" + player.Pseudo + "';";
                    while (isLooking)
                    {

                        SqlDataReader reader;
                        reader = command.ExecuteReader();
                        Console.WriteLine("waiting for player");
                        if (reader.Read())
                        {
                            game = new Game(reader.GetInt32(0));
                            game.PlayerFound = reader.GetString(1);
                            Console.WriteLine("Game found, id : " + game.Id);
                            isLooking = false;
                            player.IsInMatchMaking = false;
                            player.IsInGame = true;
                            OnMatchFound();
                            StartGameThread();
                        }

                        Thread.Sleep(1000);
                        reader.Close();
                    }

                });

                matchmakingThread.Start();
            }
            else
            {
                // on crée une partie en inscrivant le nom des 2 joueurs;
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "insert into game (pseudoplayer1, pseudoplayer2) values ('" + player.Pseudo + "','" + playerFound + "');";
                command.ExecuteNonQuery();

                Console.WriteLine("player found");

                // on retire ensuite les 2 joueurs du matchmaking
                command.CommandText = "delete from matchmaking where player = '" + player.Pseudo + "';";
                command.ExecuteNonQuery();
                command.CommandText = "delete from matchmaking where player = '" + playerFound + "';";
                command.ExecuteNonQuery();
                player.IsInMatchMaking = false;
                player.IsInGame = true;

                // start thread

                // on recupere l'id de la game qui vient d'etre créee
                SqlCommand command1 = connection.CreateCommand();
                command1.CommandText = "select id, pseudoplayer2 from game where pseudoplayer1 = '" + player.Pseudo + "';";
                SqlDataReader reader;
                reader = command1.ExecuteReader();
                if (reader.Read())
                {
                    game = new Game(reader.GetInt32(0));
                    game.PlayerFound = reader.GetString(1);
                    Console.WriteLine("Game id : " + game.Id);
                    OnMatchFound();
                    StartGameThread();
                }

                reader.Close();

                // on ecrit dans les info de la partie l'heure à laquelle celle ci va commencé pour synchroniser les joueurs.
                WriteMessage(game.Id, "begin", DateTime.Now.AddSeconds(20).ToString());
            } 
            
        }

        public void WriteMessage(int gameid, string type, string msg)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into info (gameid, messagetype, message) values (" + gameid + ",'" + type + "','" + msg + "');";
            command.ExecuteNonQuery();
        }

        public void StartGameThread()
        {
            Thread gameThread = new Thread(() =>
            {
                while (true)
                {
                    foreach (Info i in game.Infos)
                    {
                        if (!i.Read)
                        {
                            // read info
                            /* Type d'info : 
                             *  begin : times game begin at
                             *  ready : le joueur a bien chargé la partie et sera pret a l'heure indiqué
                             *  score : final score of the player
                             *  bonus : activation d'un bonus de la part d'un joueur
                             *  message : message d'information lorsqu'un joueur atteint un certain objectif 
                             *  end : le joueur a bien mis fin à la partie
                             *  left : le joueur a quitté la partie en cours
                             */
                            i.Read = true;
                            if (i.Type == "begin")
                            {
                                game.begin = Convert.ToDateTime(i.Message);
                            }
                            if (i.Type == "score")
                            {
                                
                            }
                            if (i.Type == "bonus")
                            {

                            }
                            if (i.Type == "message")
                            {

                            }
                        }
                    }
                
                }
            });
            gameThread.Start();
        }

        public void StartServerThread()
        {
            Thread serverThread = new Thread(() =>
            {
                
            });
            serverThread.Start();
        }

        public void FetchMessages()
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "select id, messagetype, message from info where gameid = " + game.Id + ";";

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                game.AddInfo(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));

            }

            reader.Close();
        }

        public Game Game
        {
            get { return game; }
        }

    }

}
