using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using Model;
using Utility;

namespace SocketClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (!usernameBox.Text.Equals(String.Empty) && !passwordBox.Password.Equals(String.Empty))
            {
                // Data buffer for incoming data.
                byte[] bytes = new byte[1024];
                BinaryConverter bConverter = new BinaryConverter();

                // Connect to a remote device.
                try
                {
                    // Establish the remote endpoint for the socket.
                    // This example uses port 11000 on the local computer.
                    IPAddress[] ipHostInfo = Dns.GetHostAddresses("127.0.0.1");
                    IPAddress ipAddress = ipHostInfo[0];
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                    // Create a TCP/IP  socket.
                    Socket client = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);

                    // Connect the socket to the remote endpoint. Catch any errors.
                    try
                    {
                        client.Connect(remoteEP);

                        Console.WriteLine("Socket connected to {0}",
                            client.RemoteEndPoint.ToString());

                        User user = new User(usernameBox.Text, passwordBox.Password);

                        // Encode the data string into a byte array.
                        byte[] authMsg = bConverter.ObjectToByteArray(user);

                        // Send the data through the socket.
                        int bytesSent = client.Send(authMsg);

                        // Receive the response from the remote device.
                        int bytesRec = client.Receive(bytes);
                        MessageBox.Show(Encoding.ASCII.GetString(bytes, 0, bytesRec));

                        // Release the socket.
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();

                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("SocketException : {0}", se.ToString());
                    }

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
            }
            else MessageBox.Show("username and password cannot be empty");
        }
        
    }
}
