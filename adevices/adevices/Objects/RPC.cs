using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Pipes;
using System.IO;

namespace adevices
{
    public class RPC
    {
        private NamedPipeServerStream pipeServer;
        public RPC()
        {
            pipeServer = new NamedPipeServerStream("testpipe", PipeDirection.InOut);

        }

        public void StartReceiver()
        {

            Common.AddToLog("NamedPipeServerStream object created.");

            // Wait for a client to connect
            Common.AddToLog("Waiting for client connection...");
            pipeServer.WaitForConnection();

            Common.AddToLog("Client connected.");
            try
            {
                // Read user input and send that to the client process.
                using (StreamWriter sw = new StreamWriter(pipeServer))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("You connected to me");
                }
            }
            // Catch the IOException that is raised if the pipe is 
            // broken or disconnected.
            catch (IOException ety)
            {
                Console.WriteLine("ERROR: {0}", ety.Message);
            }
        }
    }
}
