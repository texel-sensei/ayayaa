using ayayaa.logging.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ayayaa.logging
{
    public class LoggingServer : IDisposable
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
        private CancellationTokenSource ctSource;
        private TcpListener listener = null;
        TcpClient client = null;


        private bool DoLogging(CancellationToken cToken)
        {
            if (ip == null || port == -1)
                return false;

            IsRunning = true;

            listener = new TcpListener(ip, port);

            // Listen for incoming requests...
            listener.Start();

            while (IsRunning)
            {
                try
                {
                    // Client has been found, connecting...
                    client = listener.AcceptTcpClient();

                    // If the Server is being disposed break out of the thread. 
                    if (cToken.IsCancellationRequested)
                    {
                        break;
                    }

                    // Receiving data and buffering them...
                    NetworkStream nwStream = client.GetStream();
                    byte[] buffer = new byte[client.ReceiveBufferSize];

                    // Read stream...
                    int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                    // Convert data into string...
                    string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // Sending answer to client
                    //nwStream.Write(buffer, 0, bytesRead);

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
                    if (ex is SocketException)
                    {
                        SocketException sex = ex as SocketException;    // :smug:
                        if (sex.ErrorCode == 10004)
                        {
                            // This specific exception is called by .NET when you cancel a listener from a different thread.
                            // There might be a way to prevent it, but looking on StackOverflow & co recommends just handling the exception.
                            IsRunning = false;
                        }
                        else
                        {
                            IsRunning = false;
                            throw new LoggerException("An error occured during the receiving of client messages in the LoggingServer.", sex);
                        }
                    }
                    else
                    {
                        IsRunning = false;
                        throw new LoggerException("An error occured during the receiving of client messages in the LoggingServer.", ex);
                    }
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

            ctSource = new CancellationTokenSource();

            IsRunning = true;
            new Thread(() => DoLogging(ctSource.Token)).Start();
        }

        public void StopLogging()
        {
            IsRunning = false;
            if (listener != null)
                listener.Server.Close();
        }

        public void Dispose()
        {
            // Stop the listener from listening, thus allowing the subthread to finish manually.
            StopLogging();

            // In case the subthread didn't finish properly, murder it.
            if (ctSource != null)
            {
                ctSource.Cancel();
            }

            // Dispose of the remaining resources.
            this.listener = null;
            this.LocalLogger = null;
        }
    }
}
