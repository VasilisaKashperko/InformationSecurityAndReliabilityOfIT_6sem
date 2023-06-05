using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab10
{
    public static class RSA
    {
        static BigInteger privateKey;
        static BigInteger publicKey;
        static BigInteger modulo;
        public static void GenerateKeys()
        {
            BigInteger q = PrimeExtensions.GetRandomBigInteger(100);
            while (!PrimeExtensions.IsProbablyPrime(q))
            {
                q = PrimeExtensions.GetRandomBigInteger(100);
            }

            BigInteger p = PrimeExtensions.GetRandomBigInteger(100);
            while (!PrimeExtensions.IsProbablyPrime(p))
            {
                p = PrimeExtensions.GetRandomBigInteger(100);
            }

            BigInteger n = p * q;

            BigInteger euleuerFunctionByN = (p - 1) * (q - 1);

            BigInteger e = 17;

            BigInteger d = PrimeExtensions.ModInverse(e, euleuerFunctionByN);

            Console.WriteLine(d * e % euleuerFunctionByN == 1);

            modulo = n;
            privateKey = d;
            publicKey = e;
        }
        public static BigInteger Encrypt(BigInteger message)
        {
            return BigInteger.ModPow(message, privateKey, modulo);
        }
        public static BigInteger Decrypt(BigInteger encryptedMessage)
        {
            return BigInteger.ModPow(encryptedMessage, publicKey, modulo);
        }
    }
}
