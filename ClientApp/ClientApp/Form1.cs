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
using System.Numerics;

namespace ClientApp
{
    public partial class ClientApp : Form
    {
        public ClientApp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = IPaddress.Text;
                SendMessageFromSocket(11000, ip, "aut");
                //SendMessageFromSocket(11000);
                
            }
            finally
            {
                //Console.ReadLine();
            }

        }
        public void SendMessageFromSocket(int port, string IP, string operation)
        {
            byte[] bytes = new byte[1024];
            IPHostEntry ipHost = Dns.GetHostEntry(IP);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(ipEndPoint);

            string Login = "";
            if (operation == "aut")
            {
                Login = "aut"+" " +LoginClient.Text + " ";
            }
            byte[] loginbyte = Encoding.UTF8.GetBytes(Login);
            int bytesLoginToSend = sender.Send(loginbyte);
            // Получаем ответ от сервера
            int bytesRec = sender.Receive(bytes);
            string answerFromServer = "";
            answerFromServer += Encoding.UTF8.GetString(bytes, 0, bytesRec);
            //if (answerFromServer == "Введите пароль!")
            //{ Messanger messanger = new Messanger(); messanger.Show(); Close(); }

                        byte[] bytes2 = new byte[1024];

            if (answerFromServer == "Пользователя не существует! Зарегистрируйтесь!")
                MessageBox.Show(answerFromServer);
            else
            {
                SHA1 sha = new SHA1CryptoServiceProvider();
                //хешируем только пароль
                byte[] passwdbyteforhash = Encoding.Unicode.GetBytes(PasswordClient.Text);
                byte[] passwbeforhash = sha.ComputeHash(passwdbyteforhash);
                string PasswordHash = "";
                for (int i = 0; i < passwbeforhash.Count(); i++)
                {
                    PasswordHash += passwbeforhash[i].ToString();
                }
                //хешуруем  число и пароль
                string temp = answerFromServer + PasswordHash;
                byte[] data = Encoding.Unicode.GetBytes(temp);
                byte[] psswrdbyte = sha.ComputeHash(data);
                string NPassword = "";
                for (int i = 0; i < psswrdbyte.Count(); i++)
                {
                    NPassword += psswrdbyte[i].ToString();
                }
                byte[] NPswd = Encoding.UTF8.GetBytes(NPassword);
                int bytesSent = sender.Send(NPswd);
                int bytesRec2 = sender.Receive(bytes2);
                string answerFromServer2 = "";
                answerFromServer2 += Encoding.UTF8.GetString(bytes2, 0, bytesRec2);
                if (answerFromServer2 == "Вы вошли в свой аккаунт!")
                { Messanger messanger = new Messanger(); messanger.Show();  }
                else { MessageBox.Show("Неправильный пароль"); }
            }


            //MessageBox.Show(answerFromServer);
            // Освобождаем сокет
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 registration = new Form2();
            registration.Show();
        }

        private void IPaddress_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
