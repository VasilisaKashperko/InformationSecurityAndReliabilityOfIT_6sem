using System.Linq;

namespace Лабораторная_6
{
    public class RC4
    {
        byte[] S = new byte[256];
        int x = 0;
        int y = 0;

        public RC4(byte[] key)
        {
            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % key.Length]) % 256; // j = (j + Si + Ki) mod 256;
                S.Swap(i, j); // меняем местами        
            }
        }

        // в параметрах передается массив исходных байтов и их размер
        // для каждого байта массива исходных данных запрашивает байт ключа
        // и объединяет их при помощи xor (^)
        public byte[] Encode(byte[] dataB, int size)
        {
            byte[] data = dataB.Take(size).ToArray();
            byte[] cipher = new byte[data.Length];

            for (int m = 0; m < data.Length; m++)
            {
                cipher[m] = (byte)(data[m] ^ ItemKey());
            }
            return cipher;
        }


        // При каждом вызове отдает следующий байт ключевого потока,
        // который мы будем объединять xor'ом с байтом исходных данных
        // Генератор ПСП
        private byte ItemKey()
        {
            x = (x + 1) % 256;
            y = (y + S[x]) % 256;

            S.Swap(x, y);

            return S[(S[x] + S[y]) % 256];
        }
    }

    static class SwapExt
    {
        public static void Swap<T>(this T[] array, int index1, int index2)
        {
            T temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }
}
