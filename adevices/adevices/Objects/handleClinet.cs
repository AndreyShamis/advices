using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace adevices
{
    public class handleClinet
    {
        TcpClient clientSocket;
        string clNo;

        protected static Socket listener = null;
        public void startClient(TcpClient inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
        }

        ~handleClinet()
        {
            try
            {
                handleClinet.listener.Close();
            }
            catch (Exception ex)
            {

            }
        }

        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public static void StartListening()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 7004);

            // Create a TCP/IP socket.
            listener = new Socket(AddressFamily.InterNetwork,  SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                Common.AddToLog("handleClinet started...........");
            }
            catch (SocketException sex)
            {
                Common.AddToErrors();
                Common.AddToErrors("StartListening: ERROR (cannot be started):" + sex.Message);
                Common.AddToErrors();
            }
            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                

                //Common.ExitApplication
                while (true && !Common.ExitApplication)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Common.AddToErrors("ERROR ::::::::::::::::[StartListening]" + e.ToString());
            }
            finally
            {
                try
                {
                    listener.Close();
                }
                catch (Exception ex) { }
            }
        }

        private void APIReceiverHandler()
        {

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read more data.
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1 || content.IndexOf("$") > -1)
                {
                    content         = content.Substring(0, content.IndexOf("$"));
                    string[] cmd    = content.Split('!');
                    //Common.AddToLog("Command found : " + cmd.Length);
                    //foreach (string cmdTmp in cmd)
                    //{
                    //    Common.AddToLog("CMD:" + cmdTmp);
                    //}

                    if (cmd.Length >= 2)
                    {
                        adbdevices.AdbDevice tmp = Common.GetDeviceBySerialBumber(cmd[1]);

                        if (tmp == null)
                        {
                            Send(handler, "Fail!Device [" + cmd[1] + "] not found");
                        }
                        else if (cmd[0].Equals("DisableRecovery"))
                        {
                            tmp.IsRecoveryEnabled = false;
                            ThreadPool.QueueUserWorkItem(new WaitCallback((s) => tmp.EnbaleRecoveryInDelay(600)));
                            Send(handler, "Ok!DisableRecovery Success");
                        }
                        else if (cmd[0].Equals("EnableRecovery"))
                        {
                            tmp.IsRecoveryEnabled = true;
                            tmp.delayedRecoveryEnabled = false;
                            Send(handler, "Ok!EnableRecovery Success");
                        }
                        else if (cmd[0].Equals("DisableUSB"))
                        {
                            tmp.UsbOff();
                            Send(handler, "Ok!DisableUSB Success");
                        }
                        else if (cmd[0].Equals("EnableUSB"))
                        {
                            tmp.UsbOn();
                            Send(handler, "Ok!EnableUSB Success");
                        }
                        else
                        {
                            Send(handler, "Error!Unknown command");
                        }
                    }
                    else
                    {
                        Send(handler, "Error!Unknown command");
                    }
                    // All the data has been read from the 
                    // client. Display it on the console.
                    //Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                    //    content.Length, content);
                    // Echo the data back to the client.
                    
                }
                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }
        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


    }
}
