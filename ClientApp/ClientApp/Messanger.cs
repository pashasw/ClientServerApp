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
     public partial class Messanger : Form
    {
        public Messanger()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientApp fr = new ClientApp();
            string ip = fr.IPaddress.Text;
            SendMessageFromSocket(11000, ip, "mes");
        }
        public int LEN_OPEN_KEY_CLIENT = 70;
        public const int LEN_CLOSE_KEY_CLIENT = 60;
        

        public static string XOR(string R, string key)
        {
            string newkey = "";
            if (R.Length > key.Length)
            {
                int j = 0;
                for (; j < R.Length - key.Length; j++)
                { newkey += 0; }
                for (int i =0; i < key.Length; i++)
                { newkey += key[i]; }
            }
            if (R.Length < key.Length)
            {
                for (int i = 0; i < R.Length; i++)
                { newkey += key[i]; }
 
            }
            if (R.Length == key.Length)
            { newkey = key; }
            string answer = "";
            for (int i = 0; i < R.Length; i++)
            {
                if ((R[i] == '1' && newkey[i] == '1') || (R[i] == '0' && newkey[i] == '0'))
                    answer += '0';
                if ((R[i] == '0' && newkey[i] == '1') || (R[i] == '1' && newkey[i] == '0'))
                    answer += '1';
            }
            return answer;
        }
        public static string InByte(string input)
        {
            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                string temp = Convert.ToString(input[i], 2);

                while (temp.Length < 16)
                    temp = "0" + temp;

                output += temp;
            }

            return output;

        }// 1
        public void SendMessageFromSocket(int port, string IP, string operation)
        {

            byte[] bytes = new byte[1024];
            IPHostEntry ipHost = Dns.GetHostEntry(IP);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            string Message = InByte(MessageTextBox.Text);
            LEN_OPEN_KEY_CLIENT = MessageTextBox.Text.Length;


            sender.Connect(ipEndPoint);
            string infstring = "mes ";
            byte[] infbyte = Encoding.UTF8.GetBytes(infstring);
            int byteinf = sender.Send(infbyte);

            string OPENkey_Client = GenerateRandomNumbers.CheckIt((BigInteger.Parse(GenerateRandomNumbers.GenerateRandomNumber(LEN_OPEN_KEY_CLIENT)))).ToString();
            byte[] openkeybyte = Encoding.UTF8.GetBytes(OPENkey_Client);
            int bytesopenkeyToSend = sender.Send(openkeybyte);

            byte[] bytes2 = new byte[1024];

            int bytesRec = sender.Receive(bytes2);
            string OPENkey_Server = "";
            OPENkey_Server += Encoding.UTF8.GetString(bytes2, 0, bytesRec);
            string CLOSEkey_Client = GenerateRandomNumbers.CheckIt((BigInteger.Parse(GenerateRandomNumbers.GenerateRandomNumber(LEN_CLOSE_KEY_CLIENT)))).ToString();
            string B = BigInteger.ModPow(BigInteger.Parse(OPENkey_Server), BigInteger.Parse(CLOSEkey_Client), BigInteger.Parse(OPENkey_Client)).ToString();
            byte[] Bbyte= Encoding.UTF8.GetBytes(B);
            int bytesBToSend = sender.Send(Bbyte);

            byte[] bytes3 = new byte[1024];
            int bytesA = sender.Receive(bytes3);
            string A = "";
            A+=Encoding.UTF8.GetString(bytes3, 0, bytesA);
            string KEY = InByte(BigInteger.ModPow(BigInteger.Parse(A), BigInteger.Parse(CLOSEkey_Client), BigInteger.Parse(OPENkey_Client)).ToString());
            //string Message = InByte(MessageTextBox.Text);
            string output = XOR(Message,KEY);

            byte[] Messagebyte= Encoding.UTF8.GetBytes(output);
            int bytesMessageToSend = sender.Send(Messagebyte);

            //byte[] bytes4 = new byte[1024];
            //int bytesAnswer = sender.Receive(bytes4);
           // string Answer = "";
           // Answer += Encoding.UTF8.GetString(bytes4, 0, bytesAnswer);
            //MessageBox.Show(Answer);

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}
