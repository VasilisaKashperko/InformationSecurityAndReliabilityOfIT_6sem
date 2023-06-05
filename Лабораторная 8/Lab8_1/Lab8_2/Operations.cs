using System.Numerics;

namespace Lab8_2
{
    public class Operations
    {
        public static bool IsPRoot(BigInteger g, BigInteger n)
        {
            if (NOD(g, n) != 1)
                return false;

            if (BigInteger.ModPow(g, Euler(n) / 2, n) == 1)
                return false;

            List<BigInteger> factorisation = PrimeFactorization(g);

            foreach (var item in factorisation)
            {
                if (BigInteger.ModPow(g, item, n) == 1)
                    return false;
            }

            return true;
        }

        public static List<BigInteger> PrimeFactorization(BigInteger N)
        {
            List<BigInteger> result = new List<BigInteger>();
            for (int i = 0; N % 2 == 0; N /= 2)
            {
                result.Add(2);
            }
            for (int i = 3; i <= N;)
            {
                if (N % i == 0)
                {
                    result.Add(i);
                    N /= i;
                }
                else
                {
                    i += 2;
                }
            }
            return result;
        }

        public static BigInteger Euler(BigInteger n)
        {
            BigInteger result = n;
            for (int i = 2; i * i <= n; ++i)
                if (n % i == 0)
                {
                    while (n % i == 0)
                        n /= i;
                    result -= result / i;
                }
            if (n > 1)
                result -= result / n;
            return result;
        }

        public static BigInteger modInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }

        public static string GetBitByByte(int num)
        {
            string bit = "";
            for (int j = 8; j >= 0; j--)
                bit += num >> j & 1;
            return bit;
        }

        public static BigInteger NOD(BigInteger x, BigInteger y)
        {
            while (x != 0 && y != 0)
            {
                if (x > y)
                    x = x % y;
                else
                    y = y % x;
            }
            return (x + y);
        }
    }
}
