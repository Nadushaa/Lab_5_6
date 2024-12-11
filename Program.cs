using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_6
{
    internal class Program
    {
        /// <summary>
        /// Вывод меню
        /// </summary>
        static void PrintMenu()
        {
            Console.WriteLine(@"
1.Формирование массива и вывод его на экран. 
2.Удалить все чётные строки.
3.Формирование динамического рваного массива и вывод его на экран.
4.Добавать строку в конце массива.
5.Ввести строку символов.
6.Удалить из строки все слова, которые начинаются и заканчиваются на один и тот же символ.
7.Результаты пункта 6 вывести на печать.
8.Конец");
        }

        /// <summary>
        /// Заполнение матрицы
        /// </summary>
        static void FillMatr()
        {
            Console.WriteLine(@"Выберите способ заполнения:
    1.Ввод с помощью ДСЧ
    2.Ввод с клавиатуры");
        }
        
        /// <summary>
        /// Проверка, попадает ли число в заданный диапазон
        /// </summary>
        /// <param name="number">число</param>
        /// <param name="left">левая граница</param>
        /// <param name="right">правая граница</param>
        /// <returns></returns>
        static bool CheckDiapason(int number, int left, int right) => number >= left && number <= right;

        /// <summary>
        /// Формирование двумерного массива ДСЧ
        /// </summary>
        /// <param name="strings">кол-во строк</param>
        /// <param name="columns">кол-во столбцов</param>
        /// <returns>матрица</returns>
        static int[,] CreateMatr(int strings, int columns)
        {
            int[,] matr = new int[strings, columns];
            Random rnd = new Random();
            for (int i = 0; i<strings;  i++) 
                for (int j = 0; j<columns; j++)
                {
                    matr[i, j] = rnd.Next(0,100);
                }
            return matr;
        }

        /// <summary>
        /// Проверка массива на пустоту
        /// </summary>
        /// <param name="matr">массив</param>
        /// <returns>true or false</returns>
        static bool IsEmpty(int[,] matr)
        {
            if (matr == null || matr.Length == 0) 
                return true;
            else
                return false;
        }
        
        /// <summary>
        /// Печать двумерного массива
        /// </summary>
        /// <param name="matr">матрица</param>
        static void PrintMatr(int[,] matr, string message = "")
        {
            Console.WriteLine(message);
            if (IsEmpty(matr))
                Console.WriteLine("Матрица пустая");
            else
                for (int i = 0;i<matr.GetLength(0);i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        Console.Write(matr[i, j] + " ");
                    }
                    Console.WriteLine();
                }
        }
        
        /// <summary>
        /// Ввод двумерного массива с клавиатуры
        /// </summary>
        /// <param name="strings">строка</param>
        /// <param name="columns">столбец</param>
        /// <returns>матрица</returns>
        static int[,] CreateMatrManually(int strings, int columns)
        {
            int[,] matr = new int[strings, columns];
            Console.WriteLine("Введите элементы массива:");
            for (int i = 0; i < strings; i++)
                for (int j = 0; j < columns; j++)
                {
                    try
                    {
                        Console.WriteLine($"Элемент [{i+1},{j+1}]: ");
                        matr[i, j] = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка при вводе целого числа. Попробуйте ещё раз");
                        j--;
                    }
                }
            return matr;
        }

        /// <summary>
        /// Проверка на число
        /// </summary>
        /// <param name="message">сообщение</param>
        /// <returns>число</returns>
        static int ReadInt(string message = "")
        {
            bool isConvert;
            int number;
            do
            {
                Console.WriteLine(message);
                isConvert = int.TryParse(Console.ReadLine(), out number);
                if (!isConvert)
                    Console.WriteLine("Ошибка! Повторите ввод.");
            }while (!isConvert);
            return number;
        }
        
        /// <summary>
        /// Удаление четных строк матрицы
        /// </summary>
        /// <param name="newStrings">новое кол-во строк</param>
        /// <param name="columns">кол-во столбцов</param>
        /// <param name="strings">изначальное кол-во строк</param>
        /// <param name="matr">начальная матрица</param>
        /// <returns>новая матрица</returns>
        static int[,] DeleteItems(int newStrings, int columns, int strings, int[,] matr)
        {
            int newIndex = 0;
            int[,] newMatr = new int[newStrings, columns];
            for (int i = 0; i < strings; i+=2)
            {
                for (int j = 0; j < columns; j++)
                    newMatr[newIndex, j] = matr[i, j];
                newIndex++;
            }
            return newMatr;
        }

        /// <summary>
        /// Формирование рваного массива ДСЧ
        /// </summary>
        /// <param name="strings">кол-во строк</param>
        /// <returns>массив</returns>
        static int[][] CreateArr(int stringsArray)
        {
            Random rnd = new Random();
            int[][] array = new int[stringsArray][];
            for (int i = 0; i < stringsArray; i++)
            {
                int element = rnd.Next(1,10);
                array[i] = new int[element];
                for ( int j = 0; j < element; j++)
                {
                    array[i][j] = rnd.Next(0,100);
                }
            }
            return array;
        }
        
        /// <summary>
        /// Проверка рванного массива на пустоту
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        static bool IsEmptyArr(int[][] array)
        {
            if (array == null || array.Length == 0)
                return true;
            return false;
        }
        
        /// <summary>
        /// Ввод значений рваного массива с клавиатуры
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        static int[][] CreateArrManually(int strings)
        {
            int[][] array = new int [strings][];
            for (int i = 0;i < strings; i++)
            {
                int columns = ReadInt($"Введите количество элементов в строке {i + 1}: ");
                array[i] = new int[columns];

                Console.WriteLine($"Введите элементы для строки {i + 1}");
                for (int j = 0;j < columns; j++)
                {
                    array[i][j] = ReadInt($"Введите  {j + 1} элемент строки");
                }
            }
            return array;
        }

        /// <summary>
        /// Вывод рваного массива
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        static void PrintArr(int[][] array, string message = "")
        {
            Console.WriteLine(message);
            if (IsEmptyArr(array))
                Console.WriteLine("Массив пустой");
            else
                Console.WriteLine();
                for (int i = 0; i < array.Length; i++)
                    {
                    for (int j = 0;j < array[i].Length; j++)
                    {
                        Console.Write(array[i][j] + " ");
                    }
                    Console.WriteLine();
                }
        }
        
        /// <summary>
        /// Ввод строки для задания 4 с клавиатуры
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        static int[] CreateLineManually(int number)
        {
            int[] line = new int[number];
            for (int i = 0;i<number;i++)
            {
                Console.Write($"Введите {i + 1}-ый элемент массива: ");
                line[i] = ReadInt();
            }
            
            return line;
        }
        
        /// <summary>
        /// Cоздание строки для задания 4 с помощью ДСЧ
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        static int[] CreateLine(int number)
        {
            int []line = new int[number];
            Random rnd = new Random();
            for (int i = 0; i < number; i++)
                line[i] = rnd.Next(0, 100);
            return line;
        }
        
        /// <summary>
        /// Добавление строки в конец массива
        /// </summary>
        /// <param name="array"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        static int[][] AddLine(int[][] array, int [] line)
        {
            int[][] newArray = new int [array.Length+1][];
            array.CopyTo (newArray, 0);
            newArray[newArray.Length - 1] = line;
            return newArray;
        }

        /// <summary>
        /// Способы заполнения строки
        /// </summary>
        static void FillStr()
        {
            Console.WriteLine(@"Выберите способ заполнения строки:
    1.Ввод строки с клавиатуры.
    2.Заранее заготовленная строка.");
        }
        
        /// <summary>
        /// готовая строка
        /// </summary>
        /// <returns></returns>
        static string TestStr()
        {
            string str = "В траве сидел кузнечик! Кузнечик не трогал козявок и дружил с мухом.";
            str = str.Replace("!", " !").Replace("?"," ?").Replace("."," .").Replace(","," ,");
            return str;
        }
        
        /// <summary>
        /// Вывод строки на экран
        /// </summary>
        /// <param name="str"></param>
        static void PrintStr(string str)
        {
            str = str.Replace(" !", "!").Replace(" ?", "?").Replace(" ,", ",").Replace(" .", ".");
            Console.WriteLine(str);
        }

        
        /// <summary>
        /// Ввод строки с клавиатуры
        /// </summary>
        /// <returns></returns>
        static string CreateStr()
        {
            string str = Console.ReadLine();
            str = str.Replace("!", " !").Replace("?", " ?").Replace(".", " .").Replace(",", " ,");
            return str;
        }

        /// <summary>
        /// Удаление слов из массива
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static string DeleteWord(string str)
        {
            str = str.ToLower();
            string result = string.Join(" ", str.Split(' ')
                .Where(word => word.Length < 2 || word[0] != word[word.Length - 1]));
            return result;
        }
        
        /// <summary>
        /// Проверка строки на пустоту
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static bool IsEmptyStr(string str)
        {
            if (string.IsNullOrEmpty(str))
                return true;
            return false;
        }

        /// <summary>
        /// Вывод результаты задания 6
        /// </summary>
        /// <param name="str"></param>
        static void PrintNewStr(string str)
        {
            string[] strArr = str.Split(' ');
            bool isSentence = true;
            int i = 0;

            foreach (string word in strArr)
            {
                if (isSentence)
                {
                    strArr[i] = word[0].ToString().ToUpper()+word.Substring(1);
                    isSentence = false;
                }

                if (word.IndexOf("!") != -1)
                {
                    isSentence = true;
                }

                if (word.IndexOf(".") != -1)
                {
                    isSentence = true;
                }

                if (word.IndexOf("?") != -1)
                {
                    isSentence = true;
                }

                i++;
            }
            foreach (string word in strArr)
            {
                Console.Write($"{word} ");
            }
            Console.WriteLine();
            
        }



        static void Main(string[] args)
        {
            int answer;
            int stringsMatr = 0;
            int columnsMatr = 0;
            int stringsArray = 0;

            string strArr = "";
            string newLine = "";

            int[,] matr = new int[0,0];
            int[,] newMatr = new int[0, 0];
            int[][] array = new int[0][];

            do
            {
                PrintMenu();
                answer = ReadInt();
                if (!CheckDiapason(answer, 1, 8))
                {
                    Console.WriteLine("Такого пункта нет в меню");
                    continue;
                }
                switch (answer) 
                {
                    case 1://создание и вывод двумерного массива
                        stringsMatr = ReadInt("Введите количество строк");
                        columnsMatr = ReadInt("Введите количество столбцов");
                        FillMatr();
                        int option = ReadInt();
                        if (!CheckDiapason(option, 1, 2))
                        {
                            Console.WriteLine("Такого пункта нет в меню");
                            continue;
                        }
                        switch (option)
                        {
                            case 1:
                                Console.WriteLine();
                                matr = CreateMatr(stringsMatr, columnsMatr);//формирование значений матрицы с помощью ДСЧ
                                PrintMatr(matr, "Массив: ");
                                break;
                            case 2:
                                Console.WriteLine();
                                matr = CreateMatrManually(stringsMatr, columnsMatr);//ввод значений матрицы с клавиатуры
                                PrintMatr(matr, "Массив: ");
                                Console.WriteLine();
                                break;
                        }
                        break;
                    case 2://удаление чётных строк
                        if (!IsEmpty(matr)) //проверка двумерного массива на пустоту
                        {
                            int newStrings = (stringsMatr + 1) / 2; //кол-во строк в новой матрице
                            newMatr = DeleteItems(newStrings, columnsMatr, stringsMatr, matr);
                            matr = newMatr;
                            PrintMatr(matr);
                        }
                        else
                        {
                            Console.WriteLine("Двумерный массив пустой");
                        }
                        break;
                    case 3://формирование и вывод рваного массива
                        stringsArray = ReadInt("Введите количесвто строк рваного массива");
                        FillMatr();
                        int options = ReadInt();
                        if (!CheckDiapason(options, 1, 2))
                        {
                            Console.WriteLine("Такого пункта нет в меню");
                            continue;
                        }
                        switch (options)
                        {
                            case 1:
                                Console.WriteLine();
                                array = CreateArr(stringsArray); //формирование значений рваного массива с помощью ДСЧ
                                PrintArr(array,"Массив:");
                                Console.WriteLine();
                                break;
                            case 2:
                                Console.WriteLine();
                                array = CreateArrManually(stringsArray);//ввод значений рваного массива с клавиатуры
                                PrintArr(array, "Массив:");
                                Console.WriteLine();
                                break;
                        }
                        break;
                    case 4://добавление строки в конце массива
                        int newString = ReadInt("Введите количество элементов добавляемой строки'"); ;
                        int[] line = new int[0]; //массив вставляемой строки
                        int[][] newArray = new int[0][]; //новый массив
                        FillMatr();
                        int lineOption = ReadInt();
                        if (!CheckDiapason(lineOption, 1, 2))
                        {
                            Console.WriteLine("Такого пункта нет в меню");
                            continue;
                        }
                        switch (lineOption)
                        {
                            case 1:
                                line = CreateLine(newString);//формирование значений вставляемой строки с помощью ДСЧ
                                break;
                            case 2:
                                line = CreateLineManually(newString);//ввод значений вставляемой строки с клавиатуры
                                break;
                        }
                        newArray = AddLine(array, line);
                        array = newArray;
                        PrintArr(array, "Новый массив:");
                        break;
                    case 5://ввести строку символов
                        FillStr();
                        int action = ReadInt();
                        if (!CheckDiapason(action, 1, 2))
                        {
                            Console.WriteLine("Такого пункта нет в меню");
                            continue;
                        }
                        switch (action)
                        {
                            case 1:
                                Console.WriteLine("Введите текст: ");
                                strArr = CreateStr();//ввод строки с клавиатуры
                                Console.WriteLine("\n");
                                break;
                            case 2:
                                Console.WriteLine();
                                strArr = TestStr();//готовая строка
                                PrintStr(strArr);
                                Console.WriteLine("\n");
                                break;
                        }
                        break;
                    case 6://удалить из строки все слова, которые начинаются и заканчиваются на один и тот же символ
                        if (!IsEmptyStr(strArr))
                        {
                            Console.WriteLine();
                            newLine = DeleteWord(strArr);
                            if (newLine.Length < strArr.Length)
                                Console.WriteLine("Слова удалены\n");
                            else
                                Console.WriteLine("В строке нет элементов, которые начинаются и заканчиваются на один и тот же символ.");
                        }
                        else
                        {
                            Console.WriteLine("Строка пуста");
                        }
                        break;
                    case 7://результаты пункта 6 вывести на печать
                        if (!IsEmptyStr(newLine))//проверка на пустоту строки
                        {
                            PrintNewStr(newLine);
                        }
                        else
                        {
                            Console.WriteLine("Строка пуста");
                        }
                        break;
                }
            } while (answer != 8);
        }
    }
}
