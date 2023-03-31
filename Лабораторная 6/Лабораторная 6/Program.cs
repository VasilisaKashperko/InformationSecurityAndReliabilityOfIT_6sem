using System;
using System.Text;

namespace Лабораторная_6
{
    class Program
    {
        public static readonly int n = 253; // 11 * 23
        public static readonly int x = 5;
        public static readonly int length = 13;

        // ПСП генератор на основе квадратичных вычетов
        // Вычисляет каждое число последовательности отдельно по формуле квадратичных вычетов
        public static int BBSnext(int previous, int index)
        {
            int resultForSequence = (previous * previous) % n; // x_t= (x_(t-1))^2 mod n, вычисляемое значение генератора = (предыдущий элемент псп)^2 modn
            Console.WriteLine($"x{index} = ({previous}*{previous})mod {n} = {resultForSequence}");
            return resultForSequence;
        }

        static void Main(string[] args)
        {
            // B B S

            int[] seq = new int[length]; //хранит числа генератора псп

            Console.WriteLine($"n = {n} - число Блюма");
            Console.WriteLine($"x = {x}\n");
            int buf = x;

            long OldTicks = DateTime.Now.Ticks;

            for (int i = 0; i < length; i++)
            {
                buf = BBSnext(buf, i); //начальное значение х = 5, взаимнопростое с числом Блюма n 
                seq[i] = buf;
            }
            Console.Write("\nПСП = ");
            foreach (int item in seq)
            {
                Console.Write($"{item}; ");
            }

            Console.WriteLine($"\nВремя зашифрования: {(DateTime.Now.Ticks - OldTicks) / 1000} мс");

            // RC4

            Console.WriteLine("\n\n\n RC4 \n");

            int[] ikey = { 43, 45, 100, 21, 1 };
            byte[] key = new byte[ikey.Length];

            for (int i = 0; i < ikey.Length; i++)
            {
                key[i] = Convert.ToByte(ikey[i]);
            }

            RC4 rc = new RC4(key);
            RC4 rc2 = new RC4(key);
            byte[] testBytes = Encoding.ASCII.GetBytes("Kashperko Vasilisa");

            OldTicks = DateTime.Now.Ticks;

            byte[] encrypted = rc.Encode(testBytes, testBytes.Length);
            Console.WriteLine($"Зашифрованнное сообщение: {Encoding.ASCII.GetString(encrypted)}");

            Console.WriteLine($"Время зашифрования: {(DateTime.Now.Ticks - OldTicks) / 1000} мс");

            OldTicks = DateTime.Now.Ticks;

            byte[] decrypted = rc2.Encode(encrypted, encrypted.Length);
            Console.WriteLine($"Рашифрованнное сообщение: {Encoding.ASCII.GetString(decrypted)}");

            Console.WriteLine($"Время расшифрования: {(DateTime.Now.Ticks - OldTicks) / 1000} мс");

            Console.ReadKey();
        }
    }
}
