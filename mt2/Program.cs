using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace mt2
{


    class Program
    {
        static void Main(string[] args)
        {
            // Укажите путь к файлу, из которого нужно прочитать текст
            string filePath = @"D:\L3-204SKOCHIK\Text1.txt";

            // Проверяем, существует ли файл
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            // Читаем весь текст из файла
            string text = File.ReadAllText(filePath);

            // Получаем 10 самых часто встречающихся слов
            var topWords = GetTopWords(text, 10);

            // Выводим результаты
            Console.WriteLine("10 самых часто встречающихся слов:");
            foreach (var word in topWords)
            {
                Console.WriteLine($"{word.Key}: {word.Value}");
            }
        }

        static Dictionary<string, int> GetTopWords(string text, int topCount)
        {
            // Используем регулярное выражение для извлечения слов из текста
            var wordPattern = new Regex(@"\b\w+\b", RegexOptions.IgnoreCase);
            var matches = wordPattern.Matches(text);

            // Словарь для хранения слов и их частоты
            Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

            foreach (Match match in matches)
            {
                string word = match.Value.ToLower(); // Приводим слово к нижнему регистру

                if (wordFrequency.ContainsKey(word))
                {
                    wordFrequency[word]++;
                }
                else
                {
                    wordFrequency[word] = 1;
                }
            }

            // Сортируем словарь по частоте встречаемости слов в порядке убывания и берем топ N
            var sortedWordFrequency = wordFrequency
                .OrderByDescending(w => w.Value)
                .Take(topCount)
                .ToDictionary(w => w.Key, w => w.Value);

            return sortedWordFrequency;
        }
    }
}
