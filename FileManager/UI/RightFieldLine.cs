using System;
using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.UI
{
    public class RightFieldLine : FieldLine
    {
        public RightFieldLine(int width, List<FileItem> items) : base(width, items) { }

        public override void DrawDescription()
        {
            int lastLetter = Width - 11;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write('\u2551');
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("etc:  Имя");
            Console.ForegroundColor = ConsoleColor.Cyan;
            
            while (lastLetter > 0)
            {
                switch (lastLetter)
                {
                    case 36:
                        Console.Write('\u2502');
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("  Размер");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        lastLetter -= 9;
                        break;
                    case 24:
                        Console.Write('\u2502');
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("   Дата");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        lastLetter -= 8;
                        break;
                    case 12:
                        Console.Write('\u2502');
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("   Время");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        lastLetter -= 9;
                        break;
                    default:
                        Console.Write(" ");
                        lastLetter--;
                        break;
                }
            }
            Console.Write('\u2551');
        }

        public override int DrawData(int startIndex, int targetId, bool isTargetField)
        {
            if (startIndex >= Items.Count) return startIndex;
            
            FileItem file = Items[startIndex];
            bool fileIsTarget = (startIndex == targetId);
            int lastLetter = Width - 2;

            Console.Write('\u2551');
            
            if (fileIsTarget && isTargetField)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }

            string nameDisplay = ShortName(file.Name, 12);
            Console.Write(nameDisplay);
            lastLetter -= nameDisplay.Length;

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;

            while (lastLetter > 36)
            {
                Console.Write(" ");
                lastLetter--;
            }
            
            if (lastLetter == 36)
            {
                Console.Write('\u2502');
                lastLetter--;
            }

            string sizeDisplay = ShortName(file.IsDirectory ? "<DIR>" : file.Size.ToString(), 10);
            Console.Write(sizeDisplay);
            lastLetter -= sizeDisplay.Length;

            while (lastLetter > 24)
            {
                Console.Write(" ");
                lastLetter--;
            }
            
            if (lastLetter == 24)
            {
                Console.Write('\u2502');
                lastLetter--;
            }

            string dateDisplay = ShortName(file.ModificationDate.ToString("dd.MM.yyyy"), 10);
            Console.Write(dateDisplay);
            lastLetter -= dateDisplay.Length;

            while (lastLetter > 12)
            {
                Console.Write(" ");
                lastLetter--;
            }
            
            if (lastLetter == 12)
            {
                Console.Write('\u2502');
                lastLetter--;
            }

            string timeDisplay = ShortName(file.ModificationDate.ToString("HH:mm"), 5);
            Console.Write(timeDisplay);
            lastLetter -= timeDisplay.Length;

            while (lastLetter > 0)
            {
                Console.Write(" ");
                lastLetter--;
            }
            
            Console.Write('\u2551');
            return startIndex + 1;
        }
    }
}