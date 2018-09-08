using System;
using System.Collections.Generic;
using System.Text;
using ayayaa.connection;
using ayayaa.file_storage;
using ayayaa.message_handling;

namespace ayayaa
{
    class Server
    {
        private FileStorage _imageStorage;
        private IConnection _connection;
        private MessageDispatcher _dispatcher = new MessageDispatcher();

        Server()
        {

        }

        public void start()
        {

        }
    }
}
