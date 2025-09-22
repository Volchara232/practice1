using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Security.AccessControl;
class Program
{

    public class FileItem
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsDirectory { get; set; }
        public FileItem(string name, long size, DateTime modificationDate, bool isDirectory)
        {
            Name = name;
            Size = size;
            ModificationDate = modificationDate;
            IsDirectory = isDirectory;
        }
    }
    static string ShortenName(string name, int maxLength)    
    {
        int i = name.Length - 1, have_ext = 0;        
        string tmp, ext = "", extreverse = "";
        if (name.Length > maxLength)
        {
            while (i >= 0)
            {
                if (name[i] == '.')
                {
                    have_ext = 1;
                    break;
                }
                extreverse += name[i];
                i--;
            }          
            if (have_ext == 1)
            {   i = extreverse.Length - 1;
                while (i >= 0)
                {
                    ext += extreverse[i];
                    i--;
                }
                tmp = $"{name.Substring(0, maxLength - ext.Length - 2)}~.{ext}";
            }
            else tmp = $"{name.Substring(0, maxLength - ext.Length - 1)}~";
            return tmp;
        }
        else return name;
    }

    static void HeadBar(int width)
    {
        int last_letter = width;
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        List<string> fieldnames = new List<string>
        {
            "Левая", "Файл", "Диск", "Команды", "Правая"
        };
        Console.Write("    ");
        last_letter -= 4;
        foreach (string field in fieldnames)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(field[0]);
            last_letter--;
            Console.ForegroundColor = ConsoleColor.Black;
            if (field.Length > 1)
            {
                Console.Write(field.Substring(1));
                last_letter--;
            }
            Console.Write("    ");
            last_letter -= 4;
        }
        while (last_letter > 20)
        {
            Console.Write(" ");
            last_letter--;
        }
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.Write("8:30");
        Console.ResetColor();
    }
    static void DirInfo(int width, bool is_target)
    {
        width /= 2;
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.Cyan;
        int last_letter = width;
        int half_last_letter = (width - 12) / 2 ;
        Console.Write('\u2554');
        last_letter--;
        while (half_last_letter > 0)
        {
            Console.Write('\u2550');
            half_last_letter--;
            last_letter--;
        }
        if (is_target)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
        }
        Console.Write("  /etc/opt  ");
        Console.ResetColor();
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.Cyan;

        last_letter -= 12;
        while (last_letter > 1)
        {
            Console.Write('\u2550');
            last_letter--;
        }
        Console.Write('\u2557');    
    }
    static void CollumNames(int width)
    {
        int last_letter = width - 11;
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write('\u2551');
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("etc:  Имя");
        Console.ForegroundColor = ConsoleColor.Cyan;               
        while (last_letter > 0)
        {
            if (last_letter == 32 || last_letter == 16)
            {
                Console.Write('\u2502');
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("      Имя      ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                last_letter -= 16;
            }
            else
            {
                Console.Write(" ");
                last_letter--;
            }
        }
        Console.Write('\u2551');

        //Правая часть
       
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write('\u2551');
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("etc:  Имя");
        Console.ForegroundColor = ConsoleColor.Cyan;
        last_letter = width - 11;       
        while (last_letter > 0)
        {
            switch (last_letter)
            {
                case 36:
                    Console.Write('\u2502');
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("  Размер");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    last_letter -= 9;
                    break;
                    
                case 24:
                    Console.Write('\u2502');
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("   Дата");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    last_letter -= 8;
                    break;
                    
                case 12:
                    Console.Write('\u2502');
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("   Время");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    last_letter -= 9;
                    break;

                default:
                    Console.Write(" ");
                    last_letter--;
                    break;
            }
        }
        Console.Write('\u2551');
        Console.ResetColor(); 
        Console.WriteLine();

    }
    static void DrawBorder(int width, char left_symbol, char center_symbol, char right_symbol)
    {
        int last_letter = width - 2;
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.Cyan;        
        Console.Write(left_symbol);       
        while (last_letter > 0)
        {
            Console.Write(center_symbol);   
            last_letter--;
        }
        Console.Write(right_symbol);     
    }
    static void FileInfo(int width, FileItem file)
    {
        int last_letter = width - 2;
        Console.Write('\u2551');
        Console.Write(ShortenName(file.Name, 10));
        last_letter -= ShortenName(file.Name, 10).Length;
        while (last_letter > 35)
        {
            Console.Write(" ");
            last_letter--;
        } 
        Console.Write(ShortenName(file.IsDirectory ? "<DIR>" : file.Size.ToString(), 8));
        last_letter -= ShortenName(file.IsDirectory ? "<DIR>" : file.Size.ToString(), 8).Length;

        while (last_letter > 23)
        {
            Console.Write(" ");
            last_letter--;
        } 
        Console.Write(ShortenName(file.ModificationDate.ToString("dd.MM.yyyy"), 10));
        last_letter -= ShortenName(file.ModificationDate.ToString("dd.MM.yyyy"), 10).Length;

        while (last_letter > 11)
        {
            Console.Write(" ");
            last_letter--;
        }

        Console.Write(ShortenName(file.ModificationDate.ToString("HH:mm"), 8));
        last_letter -= ShortenName(file.ModificationDate.ToString("HH:mm"), 8).Length;

        while (last_letter > 0)
        {
            Console.Write(" ");
            last_letter--;
        }
        Console.Write('\u2551');      
    }
    static int LeftField(int width, List<FileItem> items, int i, bool file_is_target, bool field_is_target)
    {
        int last_letter = width - 2, j = 0;
        FileItem file = items[i];
        Console.Write('\u2551');
        i++;
        
        while (last_letter > 0)
        {
            if (j < file.Name.Length)
            {
                if (file_is_target & field_is_target)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                Console.Write(file.Name[j]);
                last_letter--;
                j++;
                if (j == file.Name.Length)
                {
                    file_is_target = false;
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
            }
            else if (last_letter == 32 || last_letter == 16)
            {
                Console.Write('\u2502');
                j = 0;
                last_letter--;
                file = items[i++];
                file.Name = ShortenName(file.Name, 15);
            }
            else if (j >= file.Name.Length)
            {
                Console.Write(" ");
                last_letter--;
            }
        }
        Console.Write('\u2551');
        return i;
    }
    static void RightField(int width, FileItem file, bool file_is_target, bool field_is_target)
    {
        int last_letter = width - 2;
        Console.Write('\u2551');
        if (file_is_target & field_is_target)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
        }     
        Console.Write(ShortenName(file.Name, 12));
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.Cyan;
        
        last_letter -= ShortenName(file.Name, 12).Length;
        while (last_letter > 36)
        {
            Console.Write(" ");
            last_letter--;
        }
        if (last_letter == 36)
        {
            Console.Write('\u2502');
            last_letter--;
        } 

        Console.Write(ShortenName(file.IsDirectory ? "<DIR>" : file.Size.ToString(), 10));
        last_letter -= ShortenName(file.IsDirectory ? "<DIR>" : file.Size.ToString(), 10).Length;

        while (last_letter > 24)
        {
            Console.Write(" ");
            last_letter--;
        }
        if (last_letter == 24)
        {
            Console.Write('\u2502');
            last_letter--;
        }
        Console.Write(ShortenName(file.ModificationDate.ToString("dd.MM.yyyy"), 10));
        last_letter -= ShortenName(file.ModificationDate.ToString("dd.MM.yyyy"), 10).Length;
        while (last_letter > 12)
        {
            Console.Write(" ");
            last_letter--;
        }
        if (last_letter == 12)
        {
            Console.Write('\u2502');
            last_letter--;
        }
        Console.Write(ShortenName(file.ModificationDate.ToString("HH:mm"), 5));
        last_letter -= ShortenName(file.ModificationDate.ToString("HH:mm"), 5).Length;

        while (last_letter > 0)
        {
            Console.Write(" ");
            last_letter--;
        }
        Console.Write('\u2551');
    }
    static void DataRender(int width, int height, List<FileItem> items, int id_target, int id_field_target)
    {
        int i = 0;
        bool file_is_target = false,
             left_is_target = id_field_target == 1,
             right_is_target = id_field_target == 2;   
             
        FileItem file, target = items[id_target];
        width /= 2;
        CollumNames(width);
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.Cyan;

        while (height > 7 && i < items.Count)
        {
            file = items[i];
            if (file == target) file_is_target = true;
            i = LeftField(width, items, i, left_is_target, file_is_target);
            RightField(width, file, right_is_target, file_is_target);
            Console.WriteLine();
            height--;
            file_is_target = false;
        }
        DrawBorder(width, '\u255F','\u2500', '\u2562');
        DrawBorder(width, '\u255F','\u2500', '\u2562');
        Console.WriteLine();

        FileInfo(width, target);
        FileInfo(width, target);
        Console.WriteLine();

        DrawBorder(width, '\u255A','\u2550', '\u255D');
        DrawBorder(width, '\u255A','\u2550', '\u255D');
    }
    static void FootBar(int width)
    {
        int last_letter = width;
        Console.ResetColor();
        List<string> fieldnames = new List<string>
        {
            "1Помощь", "2Вызов ", "3Чтение ", "4Правка ", "5Копия ", "6НовИмя ", "7НовКат ", "8Удал-е ", "9Меню ", "0Выход"
        };
        foreach (string field in fieldnames)
        {
            Console.Write(field[0]);
            last_letter--;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Black;
            if (field.Length > 1)
            {
                Console.Write(field.Substring(1));
                last_letter--;
            }
            Console.ResetColor();
            Console.Write("   ");
            last_letter -= 3;
        }
    }
    static void Main()
    {
        int height = 25, width = 100, id_target = 0;
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
            new FileItem("Program.cs", 2048, DateTime.Now.AddDays(-1), false),
            new FileItem("MyFileWithLongName.txt", 12345, DateTime.Now, false),
            new FileItem("Photos", 0, DateTime.Now.AddDays(-2), true),
            new FileItem("Documents", 0, DateTime.Now.AddDays(-10), true),
            new FileItem("SICRET.jpg", 2048, DateTime.Now.AddDays(-1), false),
            new FileItem("Homework", 54243, DateTime.Now, true),
            new FileItem("Anapa2007", 0, DateTime.Now.AddDays(-2), true),
            new FileItem("WiInputField", 0, DateTime.Now.AddDays(-10), true),
            new FileItem("RGR.cpp", 2048, DateTime.Now.AddDays(-1), false),
            new FileItem("PVZ.exe", 12345, DateTime.Now, false),
            new FileItem("buildZOV.exe", 0, DateTime.Now.AddDays(-2), false),
            new FileItem("Pacman", 0, DateTime.Now.AddDays(-10), true),
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
        };
        

        HeadBar(width);
        Console.WriteLine();

        DirInfo(width, false);
        DirInfo(width, true);
        Console.WriteLine();

        DataRender(width, height, items, id_target, 2);
        Console.WriteLine();
        Console.WriteLine();

        FootBar(width);
        Console.WriteLine();
        Console.SetCursorPosition(0, height - 1);
       
    }
}
