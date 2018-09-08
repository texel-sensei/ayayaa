using ayayaa.logging.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ayayaa.logging
{
    public class LoggingServer
    {
        public LoggingServer(string ip, int port) 
        {
            LocalLogger = new Logger();

            this.port = port;
            IPAddress.TryParse(ip, out this.ip);
        }

        public Logger LocalLogger { get; set; }
        public bool IsRunning { get; private set; }

        private int port = -1;
        private IPAddress ip = null;
        private Thread loggingThread = null;
        private TcpListener listener = null;

        private bool DoLogging()
        {
            if (ip == null || port == -1)
                return false;

            IsRunning = true;

            listener = new TcpListener(ip, port);
            Console.WriteLine("Listening...");

            TcpClient client = null;

            // Listen for incoming requests...
            listener.Start();

            while (IsRunning)
            {
                try
                {
                    // Client has been found, connecting...
                    client = listener.AcceptTcpClient();

                    // Receiving data and buffering them...
                    NetworkStream nwStream = client.GetStream();
                    byte[] buffer = new byte[client.ReceiveBufferSize];

                    // Read stream...
                    int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                    // Convert data into string...
                    string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received : " + dataReceived);

                    // Sending answer to client
                    Console.WriteLine("Sending back : " + dataReceived);
                    nwStream.Write(buffer, 0, bytesRead);

                    // Getting LogPriority from data
                    LogPriority priority;
                    switch (dataReceived)
                    {
                        case string a when a.Contains("TRACE"):
                            priority = LogPriority.Trace;
                            break;
                        case string b when b.Contains("DEBUG"):
                            priority = LogPriority.Debug;
                            break;
                        case string c when c.Contains("INFO"):
                            priority = LogPriority.Info;
                            break;
                        case string d when d.Contains("WARNING"):
                            priority = LogPriority.Warning;
                            break;
                        case string e when e.Contains("ERROR"):
                            priority = LogPriority.Error;
                            break;
                        case string f when f.Contains("FIRE"):
                            priority = LogPriority.FIRE;
                            break;
                        default:
                            priority = LogPriority.Info;
                            break;
                    }


                    // Writing received data to local logs
                    LocalLogger.WriteToLogs(dataReceived, priority);
                }
                catch (Exception ex)
                {
                    IsRunning = false;
                    throw new LoggerException("An error occured during the receiving of client messages in the LoggingServer.");
                }
                finally
                {
                    // Cleaning up client...
                    if (client != null)
                        client.Close();
                }
            }

            if (listener != null)
                listener.Stop();

            return true;
        }

        public void StartLogging()
        {
            if (IsRunning)
                return;

            IsRunning = true;
            loggingThread = new Thread(() => DoLogging());
            loggingThread.Start();
        }

        public void StopLogging()
        {
            IsRunning = false;
            if (listener != null)
                listener.Server.Close();
        }
    }
}
