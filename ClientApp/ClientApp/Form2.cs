using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace ClientApp
{
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClientApp fr = new ClientApp();
            if (LoginClient.Text == "")
                MessageBox.Show("Input Login!");
            if (PasswordClient.Text != PasswordAgainClient.Text)
                MessageBox.Show("Passwords is different ");
            if (PasswordClient.Text == "" || PasswordAgainClient.Text == "")
                MessageBox.Show("Input Password!");
            string ip =  fr.IPaddress.Text;
            SendMessageFromSocket(11000, ip, "reg");
        }

        public void SendMessageFromSocket(int port, string IP, string operation)
        {
            byte[] bytes = new byte[1024];
            IPHostEntry ipHost = Dns.GetHostEntry(IP);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(ipEndPoint);
            //byte[] log = Encoding.UTF8.GetBytes("reg " + LoginClient.Text+ " ");
            //int byteslog = sender.Send(log);

            string temp = PasswordClient.Text;
            byte[] data = Encoding.Unicode.GetBytes(temp);
            byte[] psswrdbyte;
            SHA1 sha = new SHA1CryptoServiceProvider();
            psswrdbyte = sha.ComputeHash(data);
            string password = "";
            for (int i = 0; i < psswrdbyte.Count(); i++)
            {
                password += psswrdbyte[i].ToString();
            }

            string InfLoginPassword = "";
            if (operation == "reg")
            {
                InfLoginPassword = "reg" + " " + LoginClient.Text + " " + password;
            }
            byte[] logPswd = Encoding.UTF8.GetBytes(InfLoginPassword);
            int bytesSent = sender.Send(logPswd);

            byte[] byteslogreg = new byte[1024];
            int bytesReclog = sender.Receive(byteslogreg);
            string existlog = "";
            existlog += Encoding.UTF8.GetString(byteslogreg, 0, bytesReclog);
            if (existlog == "Пользователь уже существует!")
            { MessageBox.Show("Введите другой Логин!"); }

            else
            {
                // Получаем ответ от сервера
                int bytesRec = sender.Receive(bytes);
                string answerFromServer = "";
                answerFromServer += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                MessageBox.Show(answerFromServer);
                Close();
            }
            // Освобождаем сокет
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}
