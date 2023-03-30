using System;

namespace Лабораторная__4
{
    class Program
    {
        static void Main()
        {
            EnigmaMachine enigmaMachine = new EnigmaMachine();

            string encodeMessage = enigmaMachine.Enrypt("KASHPERKO", 1, 0, 1);

            Console.WriteLine(encodeMessage + "\n\n");
            Console.WriteLine(enigmaMachine.Decrypt(encodeMessage, 1, 0, 1));
        }
    }
}