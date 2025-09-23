using System;
using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.UI
{
    public class LeftFieldLine : FieldLine
    {
        public LeftFieldLine(int width, List<FileItem> items) : base(width, items) { }

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
                if (lastLetter == 32 || lastLetter == 16)
                {
                    Console.Write('\u2502');
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("      Имя      ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    lastLetter -= 16;
                }
                else
                {
                    Console.Write(" ");
                    lastLetter--;
                }
            }
            Console.Write('\u2551');
        }

        public override int DrawData(int startIndex, int targetId, bool isTargetField)
        {
            int lastLetter = Width - 2;
            int currentIndex = startIndex;
            bool fileIsTarget = false;

            Console.Write('\u2551');
            
            if (currentIndex < Items.Count)
            {
                FileItem file = Items[currentIndex];
                fileIsTarget = (currentIndex == targetId);
                
                if (fileIsTarget && isTargetField)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                
                string displayName = ShortName(file.Name, 12);
                Console.Write(displayName);
                lastLetter -= displayName.Length;
                currentIndex++;
                
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            while (lastLetter > 0 && currentIndex < Items.Count)
            {
                if (lastLetter == 32 || lastLetter == 16)
                {
                    Console.Write('\u2502');
                    lastLetter--;
                    
                    FileItem file = Items[currentIndex];
                    fileIsTarget = (currentIndex == targetId);
                    
                    if (fileIsTarget && isTargetField)
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    
                    string displayName = ShortName(file.Name, 12);
                    Console.Write(displayName);
                    lastLetter -= displayName.Length;
                    currentIndex++;
                    
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.Write(" ");
                    lastLetter--;
                }
            }

            while (lastLetter > 0)
            {
                Console.Write(" ");
                lastLetter--;
                if (currentIndex >= Items.Count && (lastLetter == 32 || lastLetter == 16))
                {
                    Console.Write('\u2502');
                    lastLetter--;
                }
            }
            
            Console.Write('\u2551');
            return currentIndex;
        }
    }
}