using System;
using System.Collections.Generic;
using FileManager.Models;
using FileManager.UI;

namespace FileManager
{
    class Program
    {
        static void Main()
        {
            int height = 25, width = 100, targetId = 0, cursorHeight;
            Console.SetWindowSize(width, height); //- Не работает на Linux :(
            Console.Clear();
            List<FileItem> items = new List<FileItem>
            {
            new FileItem("MyPractice1.cs", 323, DateTime.Now.AddDays(-20), false),
            new FileItem("Program.cs", 2048, DateTime.Now.AddDays(-1), false),
            new FileItem("MyFileWithLongName.txt", 12345, DateTime.Now, false),
            new FileItem("Photos", 0, DateTime.Now.AddDays(-2), true),
            new FileItem("Documents", 0, DateTime.Now.AddDays(-10), true),
            new FileItem("SICRET.jpg", 2048, DateTime.Now.AddDays(-1), false),
            new FileItem("Homework", 54243, DateTime.Now, true),
            new FileItem("Anapa2007", 0, DateTime.Now.AddDays(-2), true),
            new FileItem("Wildberis", 0, DateTime.Now.AddDays(-10), true),
            new FileItem("RGR.cpp", 2048, DateTime.Now.AddDays(-1), false),
            new FileItem("PVZ.exe", 12345, DateTime.Now, false),
            new FileItem("buildZOV.exe", 0, DateTime.Now.AddDays(-2), false),
            new FileItem("Pacman", 0, DateTime.Now.AddDays(-10), true),
            new FileItem("report_2023.txt", 768, DateTime.Now.AddDays(-40), false),
            new FileItem("report_2024.txt", 1024, DateTime.Now.AddDays(-35), false),
            new FileItem("report_2025.txt", 1280, DateTime.Now.AddDays(-30), false),
            new FileItem("Archive.zip", 1024, DateTime.Now.AddDays(-5), false),
            new FileItem("Archive2.rar", 2048, DateTime.Now.AddDays(-3), false),
            new FileItem("Archive_backup.tar", 4096, DateTime.Now.AddDays(-1), false),
            new FileItem("alpha.txt", 100, DateTime.Now.AddDays(-1), false),
            new FileItem("ALPHA.TXT", 200, DateTime.Now.AddDays(-2), false),
            new FileItem("Alpha.doc", 300, DateTime.Now.AddDays(-3), false),
            new FileItem("AnotherExtremelyLongFileNameWithMultipleExtensions.tar.gz", 16384, DateTime.Now.AddDays(-6), false),
            new FileItem("image.png", 3072, DateTime.Now.AddDays(-8), false),
            new FileItem("document.pdf", 5120, DateTime.Now.AddDays(-12), false),
            new FileItem("video.mp4", 1048576, DateTime.Now.AddDays(-20), false),
            new FileItem("audio.mp3", 20480, DateTime.Now.AddDays(-25), false),
            };
            
            UserInterface ui = new UserInterface(width, height);

            ui.HeadBar();
            ui.DirInfo(false);
            ui.DirInfo(true);
            Console.WriteLine();

            cursorHeight = ui.DataRender(items, targetId, 2);
            Console.WriteLine();
            ui.FootBar();

            Console.SetCursorPosition(0, cursorHeight);
        }
    }
}