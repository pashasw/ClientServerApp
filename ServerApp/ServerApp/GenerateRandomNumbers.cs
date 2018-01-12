using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ServerApp
{
     class GenerateRandomNumbers 
     {
        public static BigInteger Answer;
        public static BigInteger TestNumber;
        public static BigInteger t = 0;
        public static BigInteger a = 0;
        public static BigInteger b = 0;
       public static BigInteger FirstNumber;
        public static String testik = null;


        public static void Gen (BigInteger a)
        {
            testik = GenerateRandomNumber(FirstNumber);
            TestNumber = BigInteger.Parse(testik) - 1;
        }
        public static BigInteger CheckIt(BigInteger Number)
        {
            if (TestMR(Number) == true)
            { return Number; }
            else
            {
                if (Number % 2 == 0)
                {
                    Number = Number + 3; return CheckIt(Number);
                }
                else Number = Number + 2; return CheckIt(Number);
            }
        
        }

        public static String GenerateRandomNumber(BigInteger lenght) 
        {
            int i = 0;
            String RandomNumber = null;
            Random rand = new Random();
            RandomNumber += rand.Next(1, 10).ToString();  
            i++;
            while (i < lenght)
            {
                
                RandomNumber += rand.Next(0, 10).ToString();     
                i++;
               
            }
            BigInteger temp = BigInteger.Parse(RandomNumber);
            if (temp % 2 == 0)
            {
                RandomNumber = (temp - 1).ToString();
            }
            return RandomNumber;
        }



        public static BigInteger RandomA(BigInteger lenght)   
        {
            
            Random rand = new Random();
            BigInteger lenghtA = 0;
            lenghtA = rand.Next(1, (int)(lenght + 1)); 
            String A;
            A = GenerateRandomNumber(lenghtA);
            BigInteger TestA = BigInteger.Parse(A);
            if ((1 < TestA) && (TestA < TestNumber))
            {
                return TestA;

            }
            else
            {
                return RandomA(lenght);
            }
        }

        public static bool TestMR(BigInteger Number)
        {
            BigInteger TestNumber;
            string NumberString = Number.ToString();
            int lenght = NumberString.Length;
            bool romashka = true;
            int s = 0;
            TestNumber = Number - 1;
            t = TestNumber;
            while (t % 2 == 0)
            {
                s++;
                t = TestNumber / (BigInteger)(Math.Pow(2, s));
            }

            for (int i = 0; i < 1; i++)
            {

                if (romashka == false) break;
                else
                {
                    int itter = 0;
                    a = 2;
                    if (Number % a != 0)
                    {
                        b = BigInteger.ModPow(a, t, TestNumber + 1);
                        if (b != 1)
                        {
                            while (itter < s - 1)
                            {
                                b = BigInteger.ModPow(b, 2, TestNumber + 1);

                                if (b == TestNumber)
                                { romashka = true; break; }

                                itter++;
                            }

                            if (itter == s - 1)
                            { romashka = false; }

                        }
                        else romashka = true;
                    }
                    else romashka = false;
                }
            }
            return romashka;
        }


    }
}
