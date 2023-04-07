using System.Diagnostics;

namespace Lab7
{
    class Program
    {

        public static int backpackLength = 8;

        public static List<int> superSeq = new();
        public static List<int> publicKey = new();
        public static List<string> ascList = new();
        public static List<string> decAscList = new();
        public static List<int> message = new();
        public static List<int> encryptedMessage = new();
        public static List<int> decryptMessage = new();

        static void Main(string[] args)
        {
            Stopwatch timerEncoding = new();
            timerEncoding.Start();

            string messageText = "Kashperko Vasilisa";
            int summary = 0;

            for (int i = 0; i < backpackLength; i++)
            {
                summary = superSeq.Sum();
                superSeq.Add((1 + summary));
            }
            int n = superSeq.Sum() + 1;
            int a = 2;

            while (Operations.NOD(n, a) != 1)
            {
                a++;
            }

            for (int i = 0; i < superSeq.Count; i++)
            {
                publicKey.Add((a * superSeq[i]) % n);
            }

            Console.Write("Закрытый ключ (cверхвозрастающая последовательность): ");
            foreach (var item in superSeq)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine($"\n\nВозьмем числа a = {a}, n = {n}");
            Console.WriteLine($"n > суммы элементов последовательности ({superSeq.Sum()})");

            Console.Write("\nПубличный ключ: ");

            foreach (var item in publicKey)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine("\n\nИсходное сообщение: " + messageText);
            Console.WriteLine();
            for (int i = 0; i < messageText.Length; i++)
            {
                if (Convert.ToString((int)messageText[i], 2).Length == 7)
                {
                    string asc = "0" + Convert.ToString((int)messageText[i], 2);
                    Console.WriteLine(messageText[i] + " - " + asc);
                    ascList.Add(asc);
                }
                else
                {
                    string asc = "00" + Convert.ToString((int)(int)messageText[i], 2);
                    Console.WriteLine(messageText[i] + " - " + asc);
                    ascList.Add(asc);
                }
            }
            foreach (string item in ascList)
            {
                int z = 0;
                for (int i = 0; i < item.Length; i++)
                {
                    int num = 0;
                    if (item[i] == '0')
                        num = 0;
                    else
                        num = 1;
                    z += num * publicKey[i];
                }
                encryptedMessage.Add(z);
            }

            Console.Write("\nЗашифрованное сообщение: ");
            foreach (var item in encryptedMessage)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            timerEncoding.Stop();

            Stopwatch timerDecoding = new();
            timerDecoding.Start();

            Console.Write("\nНовая последовательность: ");
            foreach (int item in encryptedMessage)
            {
                decryptMessage.Add((Operations.Inverse(a, n) * item) % n);
            }

            foreach (var item in decryptMessage)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
            Console.WriteLine();

            superSeq.Reverse();

            foreach (var item in decryptMessage)
            {
                int tmp = 0;
                string decAsc = "";
                for (int i = 0; i < superSeq.Count; i++)
                {
                    if (superSeq[i] + tmp <= item)
                    {
                        decAsc += "1";
                        tmp += superSeq[i];
                    }
                    else
                    {
                        decAsc += "0";
                    }
                }
                decAscList.Add(Operations.Reverse(decAsc));
            }

            Console.Write("ASCII: ");
            foreach (var item in decAscList)
            {
                Console.Write(item + " ");
            }

            Console.Write("\n\nРасшифрованное сообщение: ");

            foreach (var item in decAscList)
            {
                Console.Write((char)Convert.ToInt32(item, 2));
            }
            timerDecoding.Stop();

            Console.WriteLine($"\n\nВремя зашифрования: {timerEncoding.Elapsed}");
            Console.WriteLine($"Время расшифрования: {timerDecoding.Elapsed}");
            Console.ReadKey();
        }
    }
}