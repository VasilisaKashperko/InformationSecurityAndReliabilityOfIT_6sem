using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace lab10
{
    class Program
    {
        static void Main(string[] args)
        {
            string messageString = "Kashperko Vasilisa";
            Console.WriteLine(messageString);

            //хэшируем сообщение
            SHA512 shaM = new SHA512Managed();
            string messageHash = Encoding.UTF8.GetString(shaM.ComputeHash(Encoding.UTF8.GetBytes(messageString)));
            
            //RSA
            Console.WriteLine("~~~RSA~~~");
            //подписываем хешированное сообщение
            BigInteger message = new BigInteger(Encoding.UTF8.GetBytes(messageHash));

            RSA.GenerateKeys();
            BigInteger encryptedMessage = RSA.Encrypt(message);

            //дешифруем и сравниваем с хешем
            BigInteger decryptedMessage = RSA.Decrypt(encryptedMessage);

            //проверка подписи
            Console.WriteLine("подтверждено = " + (messageHash == (Encoding.UTF8.GetString(decryptedMessage.ToByteArray()))));

            //El Gamal
            Console.WriteLine("~~~El Gamal~~~");

            ElGamal elGamal = new ElGamal();

            string encryptedHash = elGamal.Encrypt(messageHash, "");
            Console.WriteLine(encryptedHash);

            string decryptedHash = elGamal.Decrypt(encryptedHash, "privateKey.txt");
            Console.WriteLine("подтверждено = " + (messageHash == decryptedHash));

            //Shnorr
            Console.WriteLine("~~~Shnorr~~~");

            Console.InputEncoding = Encoding.ASCII;
            var t = DateTime.Now;
            Shnorr.Do();
            Console.WriteLine("Shnorr:" + (DateTime.Now - t));

            //Console.WriteLine("подтверждено = " + (hash == hash3));

            Console.ReadLine();
        }
    }
}

