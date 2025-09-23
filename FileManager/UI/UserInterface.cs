using System;
using System.Collections.Generic;
using System.Linq;
using FileManager.Models;
using FileManager.UI;

namespace FileManager.UI
{
    public class UserInterface
    {
        private int Width { get; }
        private int Height { get; }

        public UserInterface(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void HeadBar()
        {
            int lastLetter = Width;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            
            List<string> fieldNames = new List<string> { "Левая", "Файл", "Диск", "Команды", "Правая" };
            Console.Write("    ");
            lastLetter -= 4;
            
            foreach (string field in fieldNames)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(field[0]);
                lastLetter--;
                Console.ForegroundColor = ConsoleColor.Black;
                
                if (field.Length > 1)
                {
                    Console.Write(field.Substring(1));
                    lastLetter -= field.Length - 1;
                }
                
                Console.Write("    ");
                lastLetter -= 4;
            }

            while (lastLetter > 4)
            {
                Console.Write(" ");
                lastLetter--;
            }

            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("8:30");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void DirInfo(bool isTarget)
        {
            int width = Width / 2;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;
            
            int lastLetter = width;
            int halfWidth = (width - 12) / 2;
            
            Console.Write('\u2554');
            lastLetter--;

            while (halfWidth > 0)
            {
                Console.Write('\u2550');
                halfWidth--;
                lastLetter--;
            }

            if (isTarget)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }

            Console.Write("  /etc/opt  ");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;

            lastLetter -= 12;

            while (lastLetter > 1)
            {
                Console.Write('\u2550');
                lastLetter--;
            }

            Console.Write('\u2557');
        }

        public void DrawBorder(char leftSymbol, char centerSymbol, char rightSymbol)
        {
            int width = Width / 2;
            int lastLetter = width - 2;
            
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(leftSymbol);

            while (lastLetter > 0)
            {
                Console.Write(centerSymbol);
                lastLetter--;
            }

            Console.Write(rightSymbol);
        }

        public void FileInfo(FileItem file)
        {
            int width = Width / 2;
            int lastLetter = width - 2;
            
            Console.Write('\u2551');
            
            string nameDisplay = FieldLine.ShortName(file.Name, 10);
            Console.Write(nameDisplay);
            lastLetter -= nameDisplay.Length;

            while (lastLetter > 35)
            {
                Console.Write(" ");
                lastLetter--;
            }

            string sizeDisplay = FieldLine.ShortName(file.IsDirectory ? "<DIR>" : file.Size.ToString(), 8);
            Console.Write(sizeDisplay);
            lastLetter -= sizeDisplay.Length;

            while (lastLetter > 23)
            {
                Console.Write(" ");
                lastLetter--;
            }

            string dateDisplay = FieldLine.ShortName(file.ModificationDate.ToString("dd.MM.yyyy"), 10);
            Console.Write(dateDisplay);
            lastLetter -= dateDisplay.Length;

            while (lastLetter > 11)
            {
                Console.Write(" ");
                lastLetter--;
            }

            string timeDisplay = FieldLine.ShortName(file.ModificationDate.ToString("HH:mm"), 8);
            Console.Write(timeDisplay);
            lastLetter -= timeDisplay.Length;

            while (lastLetter > 0)
            {
                Console.Write(" ");
                lastLetter--;
            }

            Console.Write('\u2551');
        }

        public void FootBar()
        {
            int lastLetter = Width;
            Console.ResetColor();
            
            List<string> fieldNames = new List<string>
            {
                "1Помощь", "2Вызов ", "3Чтение ", "4Правка ", "5Копия ",
                "6НовИмя ", "7НовКат ", "8Удал-е ", "9Меню ", "0Выход"
            };

            foreach (string field in fieldNames)
            {
                Console.Write(field[0]);
                lastLetter--;
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.Black;
                
                if (field.Length > 1)
                {
                    Console.Write(field.Substring(1));
                    lastLetter -= field.Length - 1;
                }
                
                Console.ResetColor();
                Console.Write("   ");
                lastLetter -= 3;
            }
            Console.WriteLine();
        }

        public int DataRender(List<FileItem> items, int targetId, int targetFieldId)
        {
            int width = Width / 2;
            int height = Height;
            int currentIndex = 0;
            int cursorHeight = 0;

            bool leftIsTarget = targetFieldId == 1;
            bool rightIsTarget = targetFieldId == 2;
            var sortedItems = TransMatrix(items.OrderBy(item => item.Name).ToList());
            var leftField = new LeftFieldLine(width, sortedItems);
            var rightField = new RightFieldLine(width, sortedItems);

            FileItem targetFile = sortedItems[targetId];
            leftField.DrawDescription();
            rightField.DrawDescription();
            Console.WriteLine();

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;      

            while (height > 7 && currentIndex < sortedItems.Count)
            {
                currentIndex = leftField.DrawData(currentIndex, targetId, leftIsTarget);
                rightField.DrawData(currentIndex - 3, targetId, rightIsTarget);
                Console.WriteLine();
                height--;
                cursorHeight++;
                currentIndex++;
            }


            DrawBorder('\u255F', '\u2500', '\u2562');
            DrawBorder('\u255F', '\u2500', '\u2562');
            Console.WriteLine();

            FileInfo(targetFile);
            FileInfo(targetFile);
            Console.WriteLine();

            DrawBorder('\u255A', '\u2550', '\u255D');
            DrawBorder('\u255A', '\u2550', '\u255D');
            Console.WriteLine();

            return cursorHeight + 6;
        }

        private static List<FileItem> TransMatrix(List<FileItem> sortedItems, int columns = 3)
        {
            int rows = (sortedItems.Count + columns - 1) / columns;
            var result = new List<FileItem>();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    int originalIndex = col * rows + row;
                    if (originalIndex < sortedItems.Count)
                        result.Add(sortedItems[originalIndex]);
                }
            }
            return result;
        }
    }
}