using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Lab11
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Задание 1
            EllipticCurve ellipticCurve = new EllipticCurve(-1, 1, 751, 728);
            
            Console.WriteLine("~~~~~~~~~~~~~~~ Задание 1.1 ~~~~~~~~~~~~~~~\n");

            int xmin = 71, xmax = 105;
            Dictionary<int, int> xValues = new Dictionary<int, int>();
            Dictionary<int, int> yValues = new Dictionary<int, int>();

            int tempX = 0;
            Console.WriteLine("Для начала вычисляем квадраты некоторых чисел по модулю 751\n\nОпределение цикличности в вычислениях на основе модулярной арифметики\n");
           
            while (tempX <= xmax)
            {
                Console.WriteLine($"\t\t\t{tempX}^2 mod 751= {(tempX * tempX) % 751} ");
                yValues.Add(tempX, (tempX * tempX) % 751);
                tempX++;
            }

            tempX = xmin;
            Console.WriteLine("\n\nЗатем рассчитываем по формуле у^2 = х^3 + aх + b (mod p)");
            while (tempX <= xmax)
            {
                Console.WriteLine($"\t\tx = {tempX} x^3-x+1 mod 751= {(tempX * tempX * tempX - tempX + 1) % 751} ");
                xValues.Add(tempX, (tempX * tempX * tempX - tempX + 1) % 751);
                tempX++;
            }

            Console.WriteLine("\n\n Выведем точки, принадлежашие кривой у^2 = х^3 - х + 1 (mod 751) :");
            foreach (var xx in xValues.Keys)
            {
                xValues.TryGetValue(xx, out int func1);
                foreach (var yy2 in yValues.Values)
                {
                    if (func1 == yy2)
                    {
                        var xx1 = yValues.FirstOrDefault(p => p.Value == yy2).Key;
                        Console.WriteLine($"\t\t\t\t({xx}, {xx1})");
                    }
                }
            }

            Dott dott = new Dott();
            for (int x = 201; x < 235; x++)
            {
                dott = new Dott(x, ellipticCurve);
            }

            Console.WriteLine("\n~~~~~~~~~~~~~~~ Задание 1.2 ~~~~~~~~~~~~~~~");
            Console.WriteLine("\n\nВыполнение операций над точками кривой\n\n");

            BigInteger k = 7;
            BigInteger l = 8;

            Dott P = new(62, 372);
            Dott Q = new(70, 195);
            Dott R = new(67, 84);

            Console.Write("k = 7\n");
            Console.Write("l = 8\n");
            Console.Write("P = ");
            Show.Dott(P);
            Console.Write("Q = ");
            Show.Dott(Q);
            Console.Write("R = ");
            Show.Dott(R);

            Console.WriteLine("\n\nПромежуточные вычисления\n\n");
            Dott k2P = Dott.DottMultiplication(P, 2, ellipticCurve);
            Console.Write("2P = ");
            Show.Dott(k2P);

            Dott k4P = Dott.DottMultiplication(P, 4, ellipticCurve);
            Console.Write("4P = ");
            Show.Dott(k4P);

            Dott k5P = Dott.DottMultiplication(P, 5, ellipticCurve);
            Console.Write("5P = ");
            Show.Dott(k5P);

            Dott k6P = Dott.DottMultiplication(P, 6, ellipticCurve);
            Console.Write("6P = ");
            Show.Dott(k6P);

            Dott kP = Dott.DottMultiplication(P, k, ellipticCurve);
            Console.Write("\n\nkP = ");
            Show.Dott(kP);

            Dott PQ = Dott.Addition(P, Q, ellipticCurve);
            Console.Write("P + Q = ");
            Show.Dott(PQ);

            Dott kPlQ_R = Dott.Addition(Dott.Addition(Dott.DottMultiplication(P, k, ellipticCurve),
                                                 Dott.DottMultiplication(Q, l, ellipticCurve),
                                                 ellipticCurve),
                                   Dott.GetDottNegativeByY(R, ellipticCurve),
                                   ellipticCurve);
            Console.Write("kP + lQ - R = ");
            Show.Dott(kPlQ_R);

            Dott P_QR = Dott.Addition(Dott.Addition(P,
                                                 Dott.GetDottNegativeByY(Q, ellipticCurve),
                                                 ellipticCurve),
                                   R,
                                   ellipticCurve);
            Console.Write("P - Q + R = ");
            Show.Dott(P_QR);
            #endregion

            #region Задание 2
            Console.WriteLine("~~~~~~~~~~~~~~~ Задание 2 ~~~~~~~~~~~~~~~\n");

            Console.WriteLine("Зашифровать/расшифровать свое собственное имя на основе ЭК");

            List<char> alphabet = new()
            {
                'А',
                'Б',
                'В',
                'Г',
                'Д',
                'Е',
                'Ж',
                'З',
                'И',
                'Й',
                'К',
                'Л',
                'М',
                'Н',
                'О',
                'П',
                'Р',
                'С',
                'Т',
                'У',
                'Ф',
                'Х',
                'Ц',
                'Ч',
                'Ш',
                'Щ',
                'Ъ',
                'Ы',
                'Ь',
                'Э',
                'Ю',
                'Я'
            };

            List<Dott> alphabetOfDotts = new()
            {
                new Dott(189, 297), //А
                new Dott(189, 454), //Б
                new Dott(192, 32),  //В
                new Dott(192, 719), //Г
                new Dott(194, 205), //Д
                new Dott(194, 546), //Е
                new Dott(197, 145), //Ж
                new Dott(197, 606), //З
                new Dott(198, 224), //И
                new Dott(198, 527), //Й
                new Dott(200, 30),  //К
                new Dott(200, 721), //Л
                new Dott(203, 324), //М
                new Dott(203, 427), //Н
                new Dott(205, 372), //О
                new Dott(205, 379), //П
                new Dott(206, 106), //Р
                new Dott(206, 645), //С
                new Dott(209, 82),  //Т
                new Dott(209, 669), //У
                new Dott(210, 31),  //Ф
                new Dott(210, 720), //Х
                new Dott(215, 247), //Ц
                new Dott(215, 504), //Ч
                new Dott(218, 150), //Ш
                new Dott(218, 601), //Щ
                new Dott(221, 138), //Ъ
                new Dott(221, 613), //Ы
                new Dott(226, 9),   //Ь
                new Dott(226, 742), //Э
                new Dott(227, 299), //Ю
                new Dott(227, 452)  //Я
            };

            string message = "ВАСИЛИСА";
            Console.WriteLine($"Исходное сообщение: {message}");

            Dott G = new Dott(0, 1);
            BigInteger d = 25;

            Console.WriteLine($"G = (0,1)\nd = 25");

            Q = Dott.DottMultiplication(G, d, ellipticCurve);
            Console.WriteLine("Открытый ключ: ");
            Show.Dott(Q);
            Console.WriteLine();

            Random rand = new Random();
            Dott C1 = new();
            Dott C2 = new();
            string messageNew = "";

            Console.WriteLine("Зашифрованное сообщение: ");
            for (int i = 0; i < message.Length; i++)
            {
                P = alphabetOfDotts[alphabet.IndexOf(message[i])];
                Console.WriteLine();
                k = 5;
                C1 = Dott.DottMultiplication(G, k, ellipticCurve);
                C2 = Dott.DottMultiplication(Q, k, ellipticCurve);
                C2 = Dott.Addition(P, C2, ellipticCurve);
                Show.Dott(C1);
                Show.Dott(C2);

                P = Dott.Addition(C2,
                                  Dott.GetDottNegativeByY(Dott.DottMultiplication(C1,
                                                                                  d,
                                                                                  ellipticCurve),
                                                                                  ellipticCurve),
                                                                                  ellipticCurve);

                messageNew += alphabet[
                                       alphabetOfDotts.IndexOf(
                                       alphabetOfDotts.Find(dd => dd.x == P.x && dd.y == P.y)
                                       )
                                       ];
            }
            Console.WriteLine("\n"+messageNew+"\n");
            #endregion

            #region Задание 3
            Console.WriteLine("\n~~~~~~~~~~~~~~~ Задание 3 ~~~~~~~~~~~~~~~");

            Console.WriteLine("\n\nГенерируем ЭЦП");

            Console.WriteLine("\nВ = (192, 32)");
            Console.WriteLine($"Первый символ собственного имени - 'В' - 192\nH(M) = 192 mod 12 = {192 % 5}");

            BigInteger data;
            data = 192 % 5;

            Dott baseDott = new Dott(416, 55);
            Console.Write("\nG = ");
            Show.Dott(baseDott);

            BigInteger q = 13;
            k = 5;
            Console.WriteLine($"\nПорядок точки G - q = {q}\n\nk = {k} (1 < k < q)");

            BigInteger privateKey = 7;
            Console.WriteLine($"Тайный ключ d = {privateKey}");

            Dott openKey = Dott.DottMultiplication(baseDott, privateKey, ellipticCurve);
            Console.Write("Открытый ключ Q: ");
            Show.Dott(openKey);

            Console.WriteLine("\n\nВерификация ЭЦП");
            Dott signature = ECDSA.CreateSignatureWithKandOrder(data, privateKey, baseDott, ellipticCurve, k, q);
            Console.Write("ЭЦП - r, s: ");
            Show.Dott(signature);

            bool isVerifired = ECDSA.VerifySignatureWithOrder(data, openKey, signature, baseDott, ellipticCurve, q);
            string verify = String.Empty;
            switch (isVerifired)
            {
                case true:
                    verify = "Успешно!";
                    break;
                case false:
                    verify = "Отклонено.";
                    break;
            }
            Console.WriteLine($"\nПроверка легитимности: \n{verify}");

            Console.ReadLine();
            #endregion
        }
    }

    public static class Show
    {
        public static void Dott(Dott dott)
        {
            Console.WriteLine("({0},{1})", dott.x, dott.y);
        }
    }
}

