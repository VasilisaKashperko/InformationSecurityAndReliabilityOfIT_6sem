using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Runtime.Remoting.Messaging;
using System.Windows.Controls.DataVisualization.Charting;

namespace Лабораторная_2
{
    public partial class MainWindow : Window
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string alphabetCaesar = "XYZVASILBCDEFGHJKMNOPQRTUW";
        string alphabetTrisemus = "KASHPEROBCDFGIJLMNQTUVWXYZ";

        private static string message, messages;
        private static string ress, deress;

        public MainWindow()
        {
            InitializeComponent();
        }

        static Dictionary<char, int> ToFrequencyDictionary(string source)
        {
            Dictionary<char, int> result = new Dictionary<char, int>();

            foreach (var symbol in source)
            {
                if (result.ContainsKey(symbol))
                    result[symbol]++;
                else
                    result[symbol] = 1;
            }

            return result;
        }

        void CesarEncrypt(string text)
        {
            if (text != null)
            {
                text = text.ToUpper();

                char[] a = alphabet.ToCharArray();
                char[] b = alphabetCaesar.ToCharArray();
                char[] c = text.ToCharArray();
                for (int i = 0; i < c.Length; i++)
                {
                    if (alphabet.Contains(c[i]))
                    {
                        int pos = alphabet.IndexOf(c[i]);
                        c[i] = b[pos];
                    }
                    outputTextBox.Text += c[i];
                }
            }
        }

        void CesarDecrypt(string text)
        {
            if (text != null)
            {
                text = text.ToUpper();
                char[] a = alphabet.ToCharArray();
                char[] b = alphabetCaesar.ToCharArray();
                char[] c = text.ToCharArray();
                for (int i = 0; i < c.Length; i++)
                {
                    if (alphabet.Contains(c[i]))
                    {
                        int pos = alphabetCaesar.IndexOf(c[i]);
                        c[i] = a[pos];
                    }
                    inputTextBox.Text += c[i];
                }
            }
        }

        void TrisemusEncrypt(string text)
        {
            if (text != null)
            {
                char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYX".ToCharArray();
                int rows = 13, columns = 2;
                String ke = "vasilisa";
                char[] keyWord = ke.ToUpper().Distinct().ToArray();
                var table = new char[rows, columns];

                for (var i = 0; i < keyWord.Length; i++)
                {
                    table[i / columns, i % columns] = keyWord[i];
                }

                alphabet = alphabet.Except(keyWord).ToArray();

                for (var i = 0; i < alphabet.Length; i++)
                {
                    int position = i + keyWord.Length;
                    table[position / columns, position % columns] = alphabet[i];
                }

                message = text.ToUpper();

                StringBuilder sb = new StringBuilder();
                for (int l = 0; l < message.Length - l; l += 255)
                {
                    message.Substring(l, l + 255);
                    var result = new char[message.Length];
                    for (var k = 0; k < message.Length; k++)
                    {
                        char symbol = message[k];
                        for (var i = 0; i < rows; i++)
                        {
                            for (var j = 0; j < columns; j++)
                            {
                                if (symbol == table[i, j])
                                {
                                    symbol = table[(i + 1) % rows, j];
                                    i = rows; 
                                    break;
                                }
                            }
                        }
                        result[k] = symbol;
                    }
                    ress = sb.Append(result).ToString();
                    break;
                }
                outputTextBox.Text = ress;
            }
        }

        void TrisemusDecrypt(string text)
        {
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYX".ToCharArray();
            int rows = 13, columns = 2;
            String ke = "eni";
            char[] keyWord = ke.ToUpper().Distinct().ToArray();
            var table = new char[rows, columns];

            for (var i = 0; i < keyWord.Length; i++)
            {
                table[i / columns, i % columns] = keyWord[i];
            }

            alphabet = alphabet.Except(keyWord).ToArray();

            for (var i = 0; i < alphabet.Length; i++)
            {
                int position = i + keyWord.Length;
                table[position / columns, position % columns] = alphabet[i];
            }

            messages = text.ToUpper();

            StringBuilder sbs = new StringBuilder();
            for (int m = 0; m < messages.Length - m; m += 255)
            {
                messages.Substring(m, m + 255);
                var results = new char[messages.Length];
                for (var k = 0; k < messages.Length; k++)
                {
                    char symbol = messages[k];
                    for (var i = 0; i < rows; i++)
                    {
                        for (var j = 0; j < columns; j++)
                        {
                            if (symbol == table[i, j])
                            {
                                symbol = table[(i + 10) % rows, j];
                                i = rows;
                                break;
                            }
                        }
                    }
                    results[k] = symbol;
                }
                deress = sbs.Append(results).ToString();
                break;
            }
        }

        private void caesarCypherButton_Click(object sender, RoutedEventArgs e)
        {
            var timer = new Stopwatch();
            timer.Start();

            outputTextBox.Text = null;
            CesarEncrypt(inputTextBox.Text);
            alphabetBefore.Text = alphabet;
            alphabetAfter.Text = alphabetCaesar;

            timer.Stop();

            MessageBox.Show($"{timer.Elapsed}", "Время шифрования (Цезарь)", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void loadFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == false)
                return;
            string filename = openFileDialog.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            inputTextBox.Text = fileText;
            MessageBox.Show("Файл открыт!", "Загрузка файла...", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void caesarDecypherButton_Click(object sender, RoutedEventArgs e)
        {
            var timer = new Stopwatch();
            timer.Start();

            inputTextBox.Text = null;
            CesarDecrypt(outputTextBox.Text);

            timer.Stop();

            MessageBox.Show($"{timer.Elapsed}", "Время дешифрования (Цезарь)", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void getDiagramsButton_Click(object sender, RoutedEventArgs e)
        {
                var frequencies = ToFrequencyDictionary(inputTextBox.Text);
                string m = null;
                string m1 = null;

                foreach (var f in frequencies)
                {
                    m += f.ToString() + "\n";
                }

            inputvaluesTextBox.Text = m;

            var frequencies2 = ToFrequencyDictionary(outputTextBox.Text);

                foreach (var f in frequencies2)
                {
                    m1 += f.ToString() + "\n";
                }

            outputvaluesTextBox.Text = m1;
        }

        private void trisemusDecypherButton_Click(object sender, RoutedEventArgs e)
        {
            var timer = new Stopwatch();
            timer.Start();

            TrisemusEncrypt(outputTextBox.Text);
            inputTextBox.Text = inputTextBox.Text.ToLower();

            timer.Stop();

            MessageBox.Show($"{timer.Elapsed}", "Время дешифрования (Трисемус)", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void trisemusCypherButton_Click(object sender, RoutedEventArgs e)
        {
            var timer = new Stopwatch();
            timer.Start();

            outputTextBox.Text = null;
            TrisemusEncrypt(inputTextBox.Text);
            alphabetBefore.Text = alphabet;
            alphabetAfter.Text = alphabetTrisemus;

            timer.Stop();

            MessageBox.Show($"{timer.Elapsed}", "Время шифрования (Трисемус)", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
