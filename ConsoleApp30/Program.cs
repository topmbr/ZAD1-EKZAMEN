using System;
using System.Collections.Generic;
using System.IO;
using System.Text;



class DictionaryApp
{
    private Dictionary<string, Dictionary<string, List<string>>> dictionaries;
    private string currentDictionary;
    public const string dictionariesDirectory = "Dictionaries/";

    public DictionaryApp()
    {
        dictionaries = new Dictionary<string, Dictionary<string, List<string>>>();
        currentDictionary = null;
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Создать словарь");
            Console.WriteLine("2. Выбрать словарь");
            Console.WriteLine("3. Добавить слово и перевод");
            Console.WriteLine("4. Заменить слово или перевод");
            Console.WriteLine("5. Удалить слово или перевод");
            Console.WriteLine("6. Искать перевод слова");
            Console.WriteLine("7. Экспортировать словарь");
            Console.WriteLine("8. Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateDictionary();
                    break;
                case "2":
                    SelectDictionary();
                    break;
                case "3":
                    AddWordAndTranslation();
                    break;
                case "4":
                    ReplaceWordOrTranslation();
                    break;
                case "5":
                    RemoveWordOrTranslation();
                    break;
                case "6":
                    SearchTranslation();
                    break;
                case "7":
                    ExportDictionary();
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения");
            Console.ReadKey();
        }
    }

    private void CreateDictionary()
    {
        Console.Write("Введите название нового словаря: ");
        string dictionaryName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(dictionaryName))
        {
            Console.WriteLine("Некорректное название словаря.");
            return;
        }

        if (dictionaries.ContainsKey(dictionaryName))
        {
            Console.WriteLine("Словарь с таким именем уже существует.");
        }
        else
        {
            dictionaries.Add(dictionaryName, new Dictionary<string, List<string>>());
            Console.WriteLine("Словарь успешно создан.");
        }
    }

    private void SelectDictionary()
    {
        Console.WriteLine("Доступные словари:");
        foreach (var dictionary in dictionaries)
        {
            Console.WriteLine("- " + dictionary.Key);
        }

        Console.Write("Введите название словаря: ");
        string dictionaryName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(dictionaryName))
        {
            Console.WriteLine("Некорректное название словаря.");
            return;
        }

        if (dictionaries.ContainsKey(dictionaryName))
        {
            currentDictionary = dictionaryName;
            Console.WriteLine("Словарь '{0}' выбран.", currentDictionary);
        }
        else
        {
            Console.WriteLine("Словарь с таким именем не существует.");
        }
    }

    private void AddWordAndTranslation()
    {
        if (currentDictionary == null)
        {
            Console.WriteLine("Сначала выберите словарь.");
            return;
        }

        Console.Write("Введите слово для добавления: ");
        string word = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(word))
        {
            Console.WriteLine("Некорректное слово.");
            return;
        }

        Console.Write("Введите перевод слова: ");
        string translation = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(translation))
        {
            Console.WriteLine("Некорректный перевод.");
            return;
        }

        if (!dictionaries[currentDictionary].ContainsKey(word))
        {
            dictionaries[currentDictionary].Add(word, new List<string>());
        }

        dictionaries[currentDictionary][word].Add(translation);
        Console.WriteLine("Слово и перевод успешно добавлены.");
    }

    private void ReplaceWordOrTranslation()
    {
        if (currentDictionary == null)
        {
            Console.WriteLine("Сначала выберите словарь.");
            return;
        }

        Console.Write("Введите слово для замены: ");
        string word = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(word))
        {
            Console.WriteLine("Некорректное слово.");
            return;
        }

        Console.Write("Введите новое слово: ");
        string newWord = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(newWord))
        {
            Console.WriteLine("Некорректное новое слово.");
            return;
        }

        if (dictionaries[currentDictionary].ContainsKey(word))
        {
            Console.Write("Введите перевод слова для замены: ");
            string translation = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(translation))
            {
                Console.WriteLine("Некорректный перевод.");
                return;
            }

            Console.Write("Введите новый перевод слова: ");
            string newTranslation = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newTranslation))
            {
                Console.WriteLine("Некорректный новый перевод.");
                return;
            }

            if (dictionaries[currentDictionary][word].Contains(translation))
            {
                dictionaries[currentDictionary][word].Remove(translation);
                dictionaries[currentDictionary][word].Add(newTranslation);
                Console.WriteLine("Слово и перевод успешно заменены.");
            }
            else
            {
                Console.WriteLine("Перевод не найден.");
            }
        }
        else
        {
            Console.WriteLine("Слово не найдено.");
        }
    }

    private void RemoveWordOrTranslation()
    {
        if (currentDictionary == null)
        {
            Console.WriteLine("Сначала выберите словарь.");
            return;
        }

        Console.Write("Введите слово для удаления: ");
        string word = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(word))
        {
            Console.WriteLine("Некорректное слово.");
            return;
        }

        if (dictionaries[currentDictionary].ContainsKey(word))
        {
            Console.WriteLine("Переводы для слова '{0}':", word);
            foreach (var translation in dictionaries[currentDictionary][word])
            {
                Console.WriteLine("- " + translation);
            }

            Console.Write("Введите перевод для удаления или оставьте пустым для удаления всего слова: ");
            string translationToRemove = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(translationToRemove))
            {
                dictionaries[currentDictionary].Remove(word);
                Console.WriteLine("Слово успешно удалено.");
            }
            else
            {
                if (dictionaries[currentDictionary][word].Contains(translationToRemove))
                {
                    dictionaries[currentDictionary][word].Remove(translationToRemove);
                    Console.WriteLine("Перевод успешно удален.");
                }
                else
                {
                    Console.WriteLine("Перевод не найден.");
                }
            }
        }
        else
        {
            Console.WriteLine("Слово не найдено.");
        }
    }

    private void SearchTranslation()
    {
        if (currentDictionary == null)
        {
            Console.WriteLine("Сначала выберите словарь.");
            return;
        }

        Console.Write("Введите слово для поиска перевода: ");
        string word = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(word))
        {
            Console.WriteLine("Некорректное слово.");
            return;
        }

        if (dictionaries[currentDictionary].ContainsKey(word))
        {
            Console.WriteLine("Переводы для слова '{0}':", word);
            foreach (var translation in dictionaries[currentDictionary][word])
            {
                Console.WriteLine("- " + translation);
            }
        }
        else
        {
            Console.WriteLine("Слово не найдено.");
        }
    }

    private void ExportDictionary()
    {
        if (currentDictionary == null)
        {
            Console.WriteLine("Сначала выберите словарь.");
            return;
        }

        string filePath = dictionariesDirectory + currentDictionary + ".txt";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var word in dictionaries[currentDictionary])
            {
                writer.WriteLine(word.Key + ":");
                foreach (var translation in word.Value)
                {
                    writer.WriteLine("- " + translation);
                }
                writer.WriteLine();
            }
        }

        Console.WriteLine("Словарь успешно экспортирован в файл '{0}'.", filePath);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Directory.CreateDirectory(DictionaryApp.dictionariesDirectory);

        DictionaryApp dictionaryApp = new DictionaryApp();
        dictionaryApp.Run();
    }

}
