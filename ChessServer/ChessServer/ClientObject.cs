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
        public string Username { get; private set; }
        TcpClient client;
        ServerObject server;
        string serverMessage;
        Random rnd = new Random();
        public string WhoIam;
        protected internal NetworkStream Stream { get; private set; }
        private bool _gameIsCreated = false;

        public ClientObject(TcpClient tcpClient, ServerObject serverObject, int Counter)
        {
            Id = Counter.ToString();
            client = tcpClient;
            if (Counter%2 == 0)
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
                Console.Write("WriteUsernameToDb(): ");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        private T_Game NewGameToDb()
        {
            if (server.clients.Count != 2) return null;

            T_Game _game = new T_Game();

            try
            {
                using (var db = new ChessDataBaseDataContext())
                {
                    var user = db.T_Users.Select(q => q).ToArray();

                    foreach (var u in user)
                    {
                        _game =
                            db.T_Games.FirstOrDefault(q => q.Black == u.Id && u.Nickname == server.clients[1].Username);
                    }

                    if (_game != null) return _game;

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
                    _game = newGame;
                    return _game;
                }
            }
            catch (Exception ex)
            {
                Console.Write("NewGameToDb(): ");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return null;
        }

        private void AddStepGame(string step, T_Game game)
        {
            try
            {
                if(game == null) return;
                using (var db = new ChessDataBaseDataContext())
                {
                    var stepArray = step.ToCharArray();
                    var count = db.T_Courses.Count(q => q.GameID == game.Id) + 1;
                    var newStep = new T_Course
                    {
                        Course = step,
                        GameID = game.Id,
                        Number = count
                    };
                    if (stepArray[0] == 'w')
                        newStep.WhoGone = game.White;
                    if (stepArray[0] == 'b')
                        newStep.WhoGone = game.Black;

                    db.T_Courses.InsertOnSubmit(newStep);
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write("AddStepGame(): ");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        public void Process()
        {
            try
            {
                int stepCounter = 0;
                Console.OutputEncoding = Encoding.Unicode;
                Stream = client.GetStream();
                string message = GetMessage();
                Username = message;

                WriteUsernameToDb(this);

                message = WhoIam;
                server.BroadcastMessage(message, this.Id);
                Console.WriteLine("{0}: присоединился к игре!", Username);

                T_Game game = NewGameToDb();
                if (game != null) _gameIsCreated = true;

                while (true)
                { 
                    try
                    {
                        message = GetMessage();
                   
                        //message = String.Format(message);

                        if (!_gameIsCreated) {
                            game = NewGameToDb();
                            _gameIsCreated = true;
                        }
                        AddStepGame(message, game);

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
                Console.Write("Process(): ");
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
