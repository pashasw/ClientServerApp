using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ServerApp
{
    class RSA
    {

        public static BigInteger E = 0;
        public static BigInteger N = 0;
        public static BigInteger FunctionOfEuler;
        public static BigInteger P = 0;
        public static BigInteger Q = 0;
        public static String testik;
        public static BigInteger D = 0;
        public static BigInteger TestNumber = 0;
        void RSAwork(BigInteger lenghtP, BigInteger lenghtQ)
        {
            E = 0; N = 0; FunctionOfEuler = 0; P = 0; Q = 0; TestNumber = 0; testik = "";
            testik = GenerateRandomNumbers.GenerateRandomNumber(lenghtP);
            P = GenerateRandomNumbers.CheckIt(BigInteger.Parse(testik));
            testik = GenerateRandomNumbers.GenerateRandomNumber(lenghtQ);
            Q = GenerateRandomNumbers.CheckIt(BigInteger.Parse(testik));
            if (P == Q) { Q = GenerateRandomNumbers.CheckIt(BigInteger.Parse(testik + 1)); }
            N = Q * P;
            FunctionOfEuler = (P - 1) * (Q - 1);

            testik = GenerateRandomNumbers.GenerateRandomNumber((N.ToString()).Length / 3);

            E = GenerateRandomNumbers.CheckIt(BigInteger.Parse(testik));
            while ((E == P) && (E == Q))
            { E = GenerateRandomNumbers.CheckIt(E + 1); }
            D = Euclid.ToCountNod(E, FunctionOfEuler);
            if (D < 0)
            {
                D = D + FunctionOfEuler;
            }
        }
    }
    
}
