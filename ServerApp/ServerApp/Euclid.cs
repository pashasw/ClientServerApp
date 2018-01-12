using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ServerApp
{
    class Euclid
    {
               public static BigInteger all;
        public static int c = 50000;
        public static BigInteger[] A = new BigInteger[c];
        public static BigInteger[] B = new BigInteger[c];
        public static BigInteger[] x = new BigInteger[c];
        public static BigInteger[] y = new BigInteger[c];
        public static BigInteger[] AdivB = new BigInteger[c];
        public static BigInteger[] AmodB = new BigInteger[c];
        public static BigInteger ToCountNod(BigInteger a, BigInteger b)
        {
            BigInteger Answer = 0;
            int i =0;
            if (a > b)
            {
                A[i] = a;
                B[i] = b;
            }
            else { A[i] = b; B[i] = a; }
            AmodB[i] = A[i] % B[i];
            AdivB[i] = A[i] / B[i];
            while (AmodB[i] != 0)
            {
                i++;
                A[i] = B[i - 1];
                B[i] = AmodB[i - 1];
                AmodB[i] = A[i] % B[i];
                AdivB[i] = A[i] / B[i];
            }
            x[i] = 0;
            y[i] = 1;
            all = i;
            while (i != 0)
            {
                x[i - 1] = y[i];
                y[i - 1] = x[i] - y[i] * AdivB[i - 1];
                i--;
            }
            Answer = x[0] * A[0] + y[0] * B[0];
            return y[0];
        }

    
    }
}
