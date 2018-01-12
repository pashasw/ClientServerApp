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
using System.Threading;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Numerics;
using System.Security.Cryptography;
namespace ServerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Thread server;
        public const int SIZE_N = 100;
        public const int LEN_OPEN_KEY_Server = 50;
        public const int LEN_CLOSE_KEY_SERVER = 60;

        public static string InfLoginPassword;

        public static IPHostEntry ipHost = Dns.GetHostEntry("localhost");
        public static IPAddress ipAddr = ipHost.AddressList[0];
        public static IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);
        public static Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        public static bool ExistLogin(string login)
        {
            string connectionStr = "Data Source=localhost;Initial Catalog=server;Integrated Security=True";//"server=localhost;user id=root; password=root;persistsecurityinfo=True;database=users";
            SqlConnection dbConnection = new SqlConnection(connectionStr);
            dbConnection.Open();

            String command = "SELECT * FROM Users AS l WHERE l.Login = '" + login + "'";
            SqlCommand cmnd = new SqlCommand(command, dbConnection);
            SqlDataReader dr = cmnd.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                i++;
            }
            dr.Close();
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public static bool ExistPassword(string login,string N ,string password)
        {
            string connectionStr = "Data Source=localhost;Initial Catalog=server;Integrated Security=True";
            SqlConnection dbConnection = new SqlConnection(connectionStr);
            dbConnection.Open();
            String command = "SELECT Password FROM Users AS l WHERE l.Login = '" + login + "'";
            SqlCommand cmnd = new SqlCommand(command, dbConnection);
            SqlDataReader dr = cmnd.ExecuteReader();
            //if (dr["Password"].ToString() == password)
            //{ return true; }
            //else return false;
            dr.Read();

            string OnTest = N + dr["Password"].ToString();
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] passwdbyteforhash = Encoding.Unicode.GetBytes(OnTest);
            byte[] passwbeforhash = sha.ComputeHash(passwdbyteforhash);
            string PasswordHash = "";
            for (int i = 0; i < passwbeforhash.Count(); i++)
            {
                PasswordHash += passwbeforhash[i].ToString();
            }

            if (PasswordHash== password) return true; 
            else return false;
           

        }
        public static void  Registration(string Login,string Password,Socket handler)
        {
            string connectionStr = "Data Source=localhost;Initial Catalog=server;Integrated Security=True";
            SqlConnection dbConnection = new SqlConnection(connectionStr);
            dbConnection.Open();

            String command = "INSERT INTO dbo.Users(Login,Password) VALUES ('" + Login + "','" + Password + "')";
            SqlCommand cmnd = new SqlCommand(command, dbConnection);
            SqlDataReader dr = cmnd.ExecuteReader();

            string reply = "Регистрация прошла успешно!";
            byte[] msg = Encoding.UTF8.GetBytes(reply);
            handler.Send(msg);
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
        public static int Conv(string a)
        {
            int len = a.Length;
            int nomber = 0;
            int[] b = new int[len];
            for (int i = 0; i < len; i++)
            {
                if (a[i] == '1')
                    b[i] = 1;
                else b[i] = 0;
            }
            for (int i = 0; i < len; i++)
            {
                nomber += b[i] * Convert.ToInt32(Math.Pow(2, len - i - 1));
            }
            return nomber;
        }
        public static string XOR(string R, string key)
        {
            string newkey = "";
            if (R.Length > key.Length)
            {
                int j = 0;
                for (; j < R.Length - key.Length; j++)
                { newkey += 0; }
                for (int i = 0; i < key.Length; i++)
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
        public void InvokeMethod()
        {
            textBoxClient.Text = InfLoginPassword;
        }
        public delegate void InvokeDelegate();
        public  void ExecuteThreadServer()
        {

            // Устанавливаем для сокета локальную конечную точку

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты

            //try
            //{
                // Начинаем слушать соединения
            sListener.Bind(ipEndPoint);
            sListener.Listen(10);
                //Socket handler = sListener.Accept();

                //textBoxServer.Text = "Ожидаем соединение через порт " + ipEndPoint;
                while (true)
                {
                    Socket handler = sListener.Accept();
                    string data = null;

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    // Показываем данные на консоли
                    InfLoginPassword = data;
                    //Action action = () => textBoxClient.Text = InfLoginPassword;
                    //if (InvokeRequired)
                    //{
                    //    Invoke(action);
                    //}
                    //else { action(); }
                   // MessageBox.Show(InfLoginPassword);
                    


                    //делим пароль и логин
                    string Inf = "";
                    string Login = "";
                    string Password = "";
                    int i = 0;
                    while (data[i] != ' ')
                    {
                        Inf += data[i];
                        i++;
                    }
                    i++;

                    if (Inf == "mes")
                    {
                        string OPENkey_Server = GenerateRandomNumbers.CheckIt((BigInteger.Parse(GenerateRandomNumbers.GenerateRandomNumber(LEN_OPEN_KEY_Server)))).ToString();
                        byte[] openkeybyte = Encoding.UTF8.GetBytes(OPENkey_Server);
                        handler.Send(openkeybyte);

                        byte[] byteOPENkey_Client = new byte[1024];
                        int bytesOPENkey = handler.Receive(byteOPENkey_Client);
                        string OPENkey_Client = "";
                        OPENkey_Client += Encoding.UTF8.GetString(byteOPENkey_Client, 0, bytesOPENkey);

                        string CLOSEkey_Server = GenerateRandomNumbers.CheckIt((BigInteger.Parse(GenerateRandomNumbers.GenerateRandomNumber(LEN_CLOSE_KEY_SERVER)))).ToString();
                        string A = BigInteger.ModPow(BigInteger.Parse(OPENkey_Server), BigInteger.Parse(CLOSEkey_Server), BigInteger.Parse(OPENkey_Client)).ToString();
                        byte[] Abyte = Encoding.UTF8.GetBytes(A);
                        handler.Send(Abyte);

                        byte[] byteB = new byte[1024];
                        int bytesB = handler.Receive(byteB);
                        string B = "";
                        B += Encoding.UTF8.GetString(byteB, 0, bytesB);

                        string KEY = InByte(BigInteger.ModPow(BigInteger.Parse(B), BigInteger.Parse(CLOSEkey_Server), BigInteger.Parse(OPENkey_Client)).ToString());
                        byte[] byteMessageCode = new byte[65536];
                        //byte[] byteMessageCode = new byte[1024];
                        int bytesMessageCode = handler.Receive(byteMessageCode);
                        string MessageCode = "";
                        MessageCode += Encoding.UTF8.GetString(byteMessageCode, 0, bytesMessageCode);

                        string Messagecode = XOR(MessageCode, KEY);
                        string Message = "";
                        for (int j = 0; j < Messagecode.Length; j = j + 16)
                        {
                            string word = "";
                            for (int k = 0; k < 16; k++)
                            {
                                word += Messagecode[k + j];
                            }
                            int number = Conv(word);
                            Message += Convert.ToChar(number);
                        }
                        Action action2 = () => textBoxClient.Text = Message;
                        if (InvokeRequired)
                        {
                            Invoke(action2);
                        }
                        else { action2(); }
                        //MessageBox.Show(Message);
                    }
                    if (Inf == "aut")
                    {
                        while (data[i] != ' ')
                        {
                            Login += data[i];
                            i++;
                        }
                        i++;

                        Random rand = new Random();
                        BigInteger lenghtA = 0;
                        lenghtA = rand.Next(1, SIZE_N);
                        string N = GenerateRandomNumbers.GenerateRandomNumber(lenghtA);

                        if (ExistLogin(Login))
                        {
                            byte[] msg = Encoding.UTF8.GetBytes(N);
                            handler.Send(msg);
                            byte[] NPassword = new byte[1024];
                            int bytesNPassword = handler.Receive(NPassword);
                            string NPasswordHash = "";
                            NPasswordHash += Encoding.UTF8.GetString(NPassword, 0, bytesNPassword);
                            if (ExistPassword(Login, N, NPasswordHash))
                            {
                                string reply = "Вы вошли в свой аккаунт!"; byte[] aut = Encoding.UTF8.GetBytes(reply);
                                handler.Send(aut);
                            }
                            else
                            {
                                string reply = "Пароль не верен!"; byte[] aut = Encoding.UTF8.GetBytes(reply);
                                handler.Send(aut);
                            }
                        }
                        else
                        {
                            string reply = "Пользователя не существует! Зарегистрируйтесь!";
                            byte[] msg = Encoding.UTF8.GetBytes(reply);
                            handler.Send(msg);
                        }


                    }
                    if (Inf == "reg")
                    {
                        while (data[i] != ' ')
                        {
                            Login += data[i];
                            i++;
                        }
                        i++;
                        if (ExistLogin(Login))
                        {
                            string reply = "Пользователь уже существует!";
                            byte[] msg = Encoding.UTF8.GetBytes(reply);
                            handler.Send(msg);
                        }
                        else
                        {
                            for (int j = i; j < data.Length; j++)
                            {
                                Password += data[j];
                            }
                            Registration(Login, Password, handler);
                            string reply = "Регистрация проршла успешно!!";
                            byte[] msg = Encoding.UTF8.GetBytes(reply);
                            handler.Send(msg);
                        }

                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            //    handler.Shutdown(SocketShutdown.Both);
            //    handler.Close();
            //}
            //finally { }
        }
        private void Start_Click(object sender, EventArgs e)
        {
            //try
            //{
                //Начинаем слушать соединения


                server.Start();


                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
            //}
            //finally { }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            //Thread.Sleep(Timeout.Infinite);
            if(server.IsAlive)
            {
                server.Interrupt();
                //server.Abort();
            }
            
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server= new Thread(ExecuteThreadServer);
        }
    }
}
