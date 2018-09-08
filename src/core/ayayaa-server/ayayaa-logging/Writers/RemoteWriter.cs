using ayayaa.logging.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ayayaa.logging.Writers
{
    public class RemoteWriter : Writer
    {
        public RemoteWriter(string ip, int port)
        {
            this.port = port;
            this.serverIP = ip;
        }

        private int port = -1;
        private string serverIP = null;
        private string comparisonMessage = string.Empty;

        public override void WriteMessage(string text)
        {
            byte[] data = SerializeMessage(text) as byte[];
            Write(data);
        }

        protected override object SerializeMessage(string message)
        {
            // In a proper file log we'll want to have a data & time for each entry, so we'll add that over here.
            // The format will be: YYYY-MM-DDTHH:mm:ss
            message = string.Format("[{0}] {1}", DateTime.Now.ToString("s"), message);

            // Save the formatted message for comparison with the server.
            comparisonMessage = message;

            // Probably unncessary, but just in case convert the string from whatever encoding we got to UTF8.
            // That way we can be sure that our logs don't have weird symbols in it.
            byte[] source = Encoding.Default.GetBytes(message);
            byte[] data = Encoding.Convert(Encoding.Default, Encoding.UTF8, source);

            return data;
        }

        protected override void Write(object text)
        {
            TcpClient client = null;

            try
            {
                byte[] data = text as byte[];
                // Create the client
                client = new TcpClient(serverIP, port);
                NetworkStream nwStream = client.GetStream();

                // Send text...
                Console.WriteLine("Sending : " + comparisonMessage);
                nwStream.Write(data, 0, data.Length);

                // Receive answer...
                byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                string answer = Encoding.UTF8.GetString(bytesToRead, 0, bytesRead);

                // Comparing received with sent message...
                if (answer != comparisonMessage)
                {
                    throw new LoggerException("An error occured during confirmation in RemoteWriter. Please check that the received and sent message are identical.");
                }
            }
            catch(Exception ex) when (ex.GetType() != typeof(LoggerException))
            {
                throw new LoggerException("An error occured while sending log message to the server in RemoteWriter.", ex);
            }
            finally
            {
                if(client != null)
                    client.Close();
            }
        }
    }
}
