using Lab8_2;
using System.Numerics;

namespace lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger p = BigInteger.Parse("6788923193742832419212371231");
            BigInteger q = BigInteger.Parse("3902847768726378127361823571");

            string Message = "Kashperko Vasilisa Sergeevna";

            RSA.algorithmRSA(p, q, Message);
            Console.WriteLine();
            ElGamal.algorithmElGamal(5935711, Message);
        }        
    }
}