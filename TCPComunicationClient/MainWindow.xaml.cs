using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace TCPComunicationClient
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private int portNumber = 8010;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();

                client.Connect(IPAddress.Parse(textBox1.Text.Trim()), portNumber);

                using (NetworkStream ns = client.GetStream())
                {
                    string sendStr = textBox.Text;
                    byte[] sendData = Encoding.Default.GetBytes(sendStr);
                    ns.Write(sendData, 0, sendData.Length);

                    byte[] recieveData = new byte[1024];
                    ns.Read(recieveData, 0, recieveData.Length);

                    var recieveStr = Encoding.UTF8.GetString(recieveData).Trim().Replace("\0", "");
                    listBox.Items.Add(recieveStr);
                }

                client.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }
}
