using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChessServer
{
    class ClientObject
    {
        protected internal string Id { get; private set; }
        //public string username;
        public string Username { get; private set; }
        TcpClient client;
        ServerObject server;
        string serverMessage;
        Random rnd = new Random();
        public string WhoIam;
        protected internal NetworkStream Stream { get; private set; }

        public ClientObject (TcpClient tcpClient, ServerObject serverObject, int Counter)
        {
            Id = Counter.ToString();
            client = tcpClient;
            if (Counter % 2 == 0)
                WhoIam = "w";
            else
                WhoIam = "b";
            server = serverObject;
            serverObject.AddConnection(this);
        }

        private void WriteUsernameToDb(ClientObject client)
        {
            try
            {
                if (client == null) return;
                using (var db = new ChessDataBaseDataContext())
                {
                    var newPlayer = new T_User
                    {
                        Nickname = client.Username
                    };

                    db.T_Users.InsertOnSubmit(newPlayer);
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }

        private void NewGameToDb()
        {
            if (server.clients.Count != 2) return;

            try
            {
                using (var db = new ChessDataBaseDataContext())
                {
                    //var users = db.T_Users.Where(q => q).ToArray();
                    var user = db.T_Users.Select(q => q).ToArray();
                    //if (user == null) return;

                    var newGame = new T_Game();

                    foreach (var cl in server.clients)
                    {
                        foreach (var u in user)
                        {
                            if (cl.Username == u.Nickname && cl.WhoIam == "w")
                                newGame.White = u.Id;
                            if (cl.Username == u.Nickname && cl.WhoIam == "b")
                                newGame.Black = u.Id;
                        }
                    }
                    db.T_Games.InsertOnSubmit(newGame);
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        public void Process()
        {
            try
            {
                Console.OutputEncoding = Encoding.Unicode;
                Stream = client.GetStream();
                string message = GetMessage();
                Username = message;

                WriteUsernameToDb(this);

                message = WhoIam;
                server.BroadcastMessage(message, this.Id);
                Console.WriteLine("{0}: присоединился к игре!", Username);

                NewGameToDb();

                while (true)
                { 
                    try
                    {
                        message = GetMessage();
                        //message = String.Format(message);
                        
                        serverMessage = String.Format("{0}: {1}", Username, message);
                        Console.WriteLine(serverMessage);
                        server.BroadcastMessage(message, this.Id);
                    }
                    catch (Exception ex)
                    {
                        message = String.Format("{0}: покинул игру", Username);
                        server.RemoveConnection(Id);
                        Console.WriteLine(message);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        private string GetMessage()
        {
            byte[] data = new byte[client.ReceiveBufferSize];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (Stream.DataAvailable);

            /*if(!Char.IsDigit(builder[2])) return builder.ToString();

            using (var db = new ChessDataBaseDataContext())
            {
                if (builder[0].ToString() == this.WhoIam)
                {
                    var 
                }

            }
            */
            return builder.ToString(); 
        }

        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
