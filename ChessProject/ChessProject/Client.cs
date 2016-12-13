using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ChessProject
{
    class Client
    {
        private const int port = 8888;
        public string WhoIam { get; private set; }
        static public bool MyStep { get; private set; }
        static public string writeMessage { get; private set; }
        static public string readStep { get; private set; }
        static public string writeStep { get; private set; }
        static public int readCurrentX { get; private set; }
        static public int readCurrentY { get; private set; }
        static public int readNewX { get; private set; }
        static public int readNewY { get; private set; }

        static TcpClient client;
        static NetworkStream stream;

        public void Connection(string userName, string ipServer)
        {
            
            try
            {
                client = new TcpClient();
                client.Connect(ipServer, port);
                stream = client.GetStream();

                writeMessage = userName;
                byte[] dataWrite = Encoding.Unicode.GetBytes(writeMessage);
                stream.Write(dataWrite, 0, dataWrite.Length);

                byte[] dataRead = new byte[client.ReceiveBufferSize];
                StringBuilder builder = new StringBuilder();
                int Bytes = 0;

                do
                {
                    Bytes = stream.Read(dataRead, 0, client.ReceiveBufferSize);
                    builder.Append(Encoding.Unicode.GetString(dataRead));
                } while (stream.DataAvailable);
                ParsingReadStep(builder);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ParsingWrightStep(string typeStatuatte, int previosX, int previosY, int newX, int newY)
        {
            char[] alpha = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            writeStep = WhoIam + alpha[previosX].ToString() + (previosY + 1).ToString() + alpha[newX].ToString() + (newY + 1).ToString() + typeStatuatte;
        }
        public void SendMessage(string typeStatuatte, int previosX, int previosY, int newX, int newY)
        {
            try
            {
                ParsingWrightStep(typeStatuatte, previosX, previosY, newX, newY);
                byte[] data = Encoding.Unicode.GetBytes(writeStep);
                stream.Write(data, 0, data.Length);
                if (MyStep)
                    MyStep = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ReciveMessage()
        {
            try
            {
                byte[] data = new byte[client.ReceiveBufferSize];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(data, 0, client.ReceiveBufferSize);
                    builder.Append(Encoding.Unicode.GetString(data));
                } while (stream.DataAvailable);
                ParsingReadStep(builder);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }
        public void ParsingReadStep(StringBuilder data)
        {
            if (data[0].ToString() == "w" && data[1].ToString() == "\0")
            {
                WhoIam = "w";
                MyStep = true;
            }
            else if (data[0].ToString() == "b" && data[1].ToString() == "\0")
            {
                WhoIam = "b";
                MyStep = false;
            }
            if (data[0].ToString() == "w" && data[1].ToString() != "\0")
            {
                WhoIam = "b";
                MyStep = true;
            }
            else if (data[0].ToString() == "b" && data[1].ToString() != "\0")
            {
                WhoIam = "w";
                MyStep = false;
            }
            if (data[1].ToString() != "\0")
            {
                for (int i = 1; i < 5; i++)
                {
                    if (i == 1 || i == 3)
                    {
                        switch (data[i].ToString())
                        {
                            case "a":
                                if (i == 1)
                                    readCurrentX = 0;
                                else
                                    readNewX = 0;
                                break;
                            case "b":
                                if (i == 1)
                                    readCurrentX = 1;
                                else
                                    readNewX = 1;
                                break;
                            case "c":
                                if (i == 1)
                                    readCurrentX = 2;
                                else
                                    readNewX = 2;
                                break;
                            case "d":
                                if (i == 1)
                                    readCurrentX = 3;
                                else
                                    readNewX = 3;
                                break;
                            case "e":
                                if (i == 1)
                                    readCurrentX = 4;
                                else
                                    readNewX = 4;
                                break;
                            case "f":
                                if (i == 1)
                                    readCurrentX = 5;
                                else
                                    readNewX = 5;
                                break;
                            case "g":
                                if (i == 1)
                                    readCurrentX = 6;
                                else
                                    readNewX = 6;
                                break;
                            case "h":
                                if (i == 1)
                                    readCurrentX = 7;
                                else
                                    readNewX = 7;
                                break;
                        }
                    }
                    else if (i == 2 || i == 4)
                    {
                        if (i == 2)
                            readCurrentY = Convert.ToInt32(data[i].ToString()) - 1;
                        else
                            readNewY = Convert.ToInt32(data[i].ToString()) - 1;
                    }

                }
            }
        }
        static void Disconnect()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
            Environment.Exit(0);
        }

    }

}
