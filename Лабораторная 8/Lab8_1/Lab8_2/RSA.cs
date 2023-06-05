using System.Diagnostics;
using System.Numerics;

namespace Lab8_2
{
    public class RSA
    {
        public static void algorithmRSA(BigInteger p, BigInteger q, string Message)
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~RSA~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Stopwatch stopwatchEnc = new();
            stopwatchEnc.Start();

            Console.WriteLine("p = " + p);
            Console.WriteLine("q = " + q);

            Random rand = new();
            BigInteger n = p * q;
            Console.WriteLine("\nn = " + n);

            BigInteger e = q * p / rand.Next(1, 19);
            for (; e > 0; e--)
            {
                if (Operations.NOD(e, (p - 1) * (q - 1)) == 1)
                {
                    break;
                }
            }
            Console.WriteLine("e = " + e);

            BigInteger d = Operations.modInverse(e, (p - 1) * (q - 1));
            Console.WriteLine("d = " + d);

            string[] SplitMessage = Message.Split(' ');
            List<BigInteger> BlockMessage = new();

            for (int i = 0; i < SplitMessage.Length; i++)
            {
                BlockMessage.Add(0);
                for (int j = 0; j < SplitMessage[i].Length; j++)
                {
                    BlockMessage[i] += Convert.ToInt32(SplitMessage[i][j]);

                    if (j != SplitMessage[i].Length - 1)
                    {
                        BlockMessage[i] *= 1000;
                    }
                }
            }

            Console.WriteLine("\nСообщение:");
            Console.WriteLine(Message);
            foreach (var item in BlockMessage)
            {
                Console.WriteLine(item);
            }

            List<BigInteger> encryptArray = new();
            long ellapledTicks = DateTime.Now.Ticks;

            Console.WriteLine("\nЗашифрованный текст:");
            foreach (var item in BlockMessage)
            {
                encryptArray.Add(BigInteger.ModPow(item, e, n));
                Console.WriteLine(encryptArray[encryptArray.Count - 1]);
            }

            stopwatchEnc.Stop();
            Stopwatch stopwatchDec = new();
            stopwatchDec.Start();

            List<BigInteger> decryptArray = new();
            ellapledTicks = DateTime.Now.Ticks;

            foreach (var item in encryptArray)
            {
                decryptArray.Add(BigInteger.ModPow(item, d, n));
            }

            Console.WriteLine("\nРасшифрованное сообщение:");
            string stringDecryptMessage = "";
            for (int i = 0; i < decryptArray.Count; i++)
            {
                stringDecryptMessage += Convert.ToChar(Convert.ToInt32(decryptArray[i].ToString().Substring(0, decryptArray[i].ToString().Length % 3)));

                for (int j = decryptArray[i].ToString().Length % 3; j < decryptArray[i].ToString().Length; j += 3)
                {
                    stringDecryptMessage += Convert.ToChar(Convert.ToInt32(decryptArray[i].ToString().Substring(j, 3)));
                }

                stringDecryptMessage += " ";
            }
            Console.WriteLine(stringDecryptMessage);

            stopwatchDec.Stop();

            Console.WriteLine($"\nВремя зашифрования (RSA): {stopwatchEnc.Elapsed}");
            Console.WriteLine($"Время расшифрования (RSA): {stopwatchDec.Elapsed}");
        }
    }
}
