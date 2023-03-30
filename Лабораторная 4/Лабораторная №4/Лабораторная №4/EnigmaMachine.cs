using System.Text;

namespace Лабораторная__4
{
    public class EnigmaMachine
    {
            private static readonly string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            private static readonly string _rotor1 = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
            private static readonly string _rotor7 = "NZJHGRCXMYSWBOUFAIVLPEKQDT";
            private static readonly string _rotor3 = "BDFHJLCPRTXVZNYEIWGAKMUSQO";

            private static readonly string[] _reflectorB = { "AY", "BR", "CU", "DH", "EQ", "FS", "GL", "IP", "JX", "KN", "MO", "TZ", "VW" };

            public string Enrypt(string text, int posL, int posM, int posR)
            {
                var rotorR = new EnigmaRotor(_rotor7, posR);
                var rotorM = new EnigmaRotor(_rotor3, posM);
                var rotorL = new EnigmaRotor(_rotor1, posL);

                StringBuilder result = new StringBuilder(text.Length);

                foreach (var ch in text)
                {
                    Console.WriteLine("\t\t\t"+ch);

                    char symbol = rotorR[_alphabet.IndexOf(ch)];
                    WriteConsole(symbol);

                    symbol = rotorM[_alphabet.IndexOf(symbol)];
                    WriteConsole(symbol);

                    symbol = rotorL[_alphabet.IndexOf(symbol)];
                    WriteConsole(symbol);

                    symbol = _reflectorB.First(x => x.Contains(symbol)).First(x => !x.Equals(symbol));
                    WriteConsole(symbol);

                    symbol = rotorL[_alphabet.IndexOf(symbol)];
                    WriteConsole(symbol);

                    symbol = rotorM[_alphabet.IndexOf(symbol)];
                    WriteConsole(symbol);

                    symbol = rotorR[_alphabet.IndexOf(symbol)];
                    WriteConsole(symbol);

                    Console.WriteLine();
                    result.Append(symbol);
                }

                Console.WriteLine("\n");
                return result.ToString();
            }

            public void WriteConsole(char symbol)
            {
                Console.Write(" ---> " + symbol);
            }

            public string Decrypt(string text, int posL, int posM, int posR)
            {
                var rotorR = new EnigmaRotor(_rotor7, posR);
                var rotorM = new EnigmaRotor(_rotor3, posM);
                var rotorL = new EnigmaRotor(_rotor1, posL);

                StringBuilder result = new StringBuilder(text.Length);

                foreach (var charSymbol in text)
                {
                    Console.WriteLine("\t\t\t" + charSymbol);

                    char symbol = _alphabet[rotorR.IndexOf(charSymbol)];
                    WriteConsole(symbol);

                    symbol = _alphabet[rotorM.IndexOf(symbol)];
                    WriteConsole(symbol);

                    symbol = _alphabet[rotorL.IndexOf(symbol)];
                    WriteConsole(symbol);

                    symbol = _reflectorB.First(x => x.Contains(symbol)).First(x => !x.Equals(symbol));
                    WriteConsole(symbol);

                    symbol = _alphabet[rotorL.IndexOf(symbol)];
                    WriteConsole(symbol);

                    symbol = _alphabet[rotorM.IndexOf(symbol)];
                    WriteConsole(symbol);

                    symbol = _alphabet[rotorR.IndexOf(symbol)];
                    WriteConsole(symbol);

                    Console.WriteLine();
                    result.Append(symbol);
                }

                Console.WriteLine("\n");
                return result.ToString();
            }
    }
}
