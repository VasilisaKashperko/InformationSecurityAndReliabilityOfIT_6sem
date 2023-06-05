using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

Stopwatch stopWatch = new();
Console.WriteLine("Введите строку :");

var md5 = MD5.Create();
var sha = SHA256.Create();

string text = Console.ReadLine();

stopWatch.Start();
var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
var hash2 = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
stopWatch.Stop();

TimeSpan ts = stopWatch.Elapsed;

string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts.Hours,
    ts.Minutes,
    ts.Seconds,
    ts.Milliseconds / 10);

Console.WriteLine($"\n~Результат SHA256: {Convert.ToBase64String(hash2)}");
Console.WriteLine($"\n~Результат MD5: {Convert.ToBase64String(hash)}");
Console.WriteLine($"\n~Затраченное время: {elapsedTime}");