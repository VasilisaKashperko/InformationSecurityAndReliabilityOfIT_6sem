using System.Diagnostics;
using System.Numerics;

namespace Lab8_2
{
    public class ElGamal
    {
        public static void algorithmElGamal(BigInteger p, string Message)
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~El-Gamal~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Stopwatch stopwatchEnc = new();
            stopwatchEnc.Start();

            var rand = new Random();

            BigInteger g = p - rand.Next() % p;
            for (; g > 0; g--)
            {
                if (Operations.IsPRoot(g, p))
                    break;
            }
            BigInteger x = rand.Next() % p;

            var encrypt = "";
            var y = BigInteger.ModPow(g, x, p);

            Console.WriteLine("Открытые числа p и g");
            Console.WriteLine("p = " + p + "- некоторое большое простое число");
            Console.WriteLine("g = " + g + "- (1 < g < p-1)");
            
            Console.WriteLine();
            Console.WriteLine("x = " + x);
            Console.WriteLine("y = " + y);

            long ellapledTicks = DateTime.Now.Ticks;

            foreach (int code in Message)
                if (code > 0)
                {
                    var k = rand.Next() % (p - 2) + 1;
                    var a = BigInteger.ModPow(g, k, p);
                    var b = BigInteger.ModPow(BigInteger.ModPow(y, k, p) * code, 1, p);
                    encrypt += a + " " + b + " ";
                }

            Console.WriteLine("\nЗашифрованное сообщение: \n" + encrypt);

            stopwatchEnc.Stop();
            Stopwatch stopwatchDec = new();
            stopwatchDec.Start();

            var decrypt = "";
            var arr = encrypt.Split(' ').Where(xx => xx != "").ToArray();
            for (var i = 0; i < arr.Length; i += 2)
            {
                BigInteger a = BigInteger.Parse(arr[i]);
                BigInteger b = BigInteger.Parse(arr[i + 1]);

                if (a != 0 && b != 0)
                {
                    var deM = BigInteger.ModPow(b * BigInteger.ModPow(a, p - 1 - x, p), 1, p);
                    var m = (char)deM;
                    decrypt += m;
                }
            }
            Console.WriteLine("\nРасшифрованное сообщение: \n" + decrypt);

            stopwatchDec.Stop();

            Console.WriteLine($"\nВремя зашифрования (ElGamal): {stopwatchEnc.Elapsed}");
            Console.WriteLine($"Время расшифрования (ElGamal): {stopwatchDec.Elapsed}");
        }
    }
}
