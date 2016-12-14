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
        string username;
        TcpClient client;
        ServerObject server;
        string serverMessage;
        Random rnd = new Random();
        string WhoIam;
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
        public void Process()
        {
            try
            {
                Console.OutputEncoding = Encoding.Unicode;
                Stream = client.GetStream();
                string message = GetMessage();
                username = message;

                message = WhoIam;
                server.BroadcastMessage(message, this.Id);
                Console.WriteLine("{0}: присоединился к игре!", username);

                while (true)
                    try
                    {
                        message = GetMessage();
                        message = String.Format("{0}{1}", WhoIam, message);
                        serverMessage = String.Format("{0}: {1}", username, message);
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.Id);
                    }
                    catch (Exception ex)
                    {
                        message = String.Format("{0}: покинул игру", username);
                        Console.WriteLine(message);
                        break;
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
