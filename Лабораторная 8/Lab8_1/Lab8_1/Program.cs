using System;
using System.Diagnostics;

namespace Lab8_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] aValues = { 5, 25 };
            int[] xValues = { 10007, 20483, 40961, 65537, 131071, 262147, 524287, 1048573, 2088571, 4176901 };
            int[] nValues = { 1024, 2048 };

            Console.WriteLine("|   a   |      x      |    n    |    y   |  Time, ms |");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            foreach (int a in aValues)
            {
                foreach (int x in xValues)
                {
                    foreach (int n in nValues)
                    {
                        Stopwatch stopwatch = new Stopwatch();

                        stopwatch.Start();

                        long y = ModuleCalculation(a, x, n);

                        Thread.Sleep(10);

                        stopwatch.Stop();

                        Console.WriteLine($"| {a,4}  | {x,10}  | {n,6}  | {y,5}  | {stopwatch.ElapsedMilliseconds,9} |");
                    }
                }
            }

            Console.ReadKey();
        }

        static long ModuleCalculation(int a, int x, int n)
        {
            long result = 1;
            long power = a;

            while (x > 0)
            {
                if ((x & 1) == 1)
                {
                    result = (result * power) % n;
                }
                power = (power * power) % n;
                x >>= 1;
            }

            return result;
        }
    }
}