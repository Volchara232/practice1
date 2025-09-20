using System;
using System.Collections.Generic;
using System.IO;
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
                ext += name[i];
                i--;
            }
            

            if (have_ext == 1)
            {   i = ext.Length - 1;
                while (i >= 0)
                {
                    extreverse += ext[i];
                    i--;
                }
                tmp = $"{name.Substring(0, maxLength - extreverse.Length - 2)}~.{extreverse}";
            }
            else tmp = $"{name.Substring(0, maxLength - extreverse.Length - 1)}~";
            return tmp;

        }
        else return name;
    }
    

    
    static void FirstInterface(int width)
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
            last_letter -= 1;


            Console.ForegroundColor = ConsoleColor.Black;

            if (field.Length > 1)
            {
                Console.Write(field.Substring(1));
                last_letter -= 1;
            }



            Console.Write("    ");
            last_letter -= 4;
        }


        while (last_letter > 20)
        {
            Console.Write(" ");
            last_letter -= 1;
        }
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.Write("8:30");

        Console.ResetColor();
        Console.WriteLine();


    }
    static void FirstLinefield(int width)
    {
        width /= 2;
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.Cyan;
        int last_letter = width;
        int half_last_letter = (last_letter - 12) / 2 ;

        Console.Write('\u2554');
        last_letter -= 1;


        while (half_last_letter > 0)
        {
            Console.Write('\u2550');
            half_last_letter -= 1;
            last_letter -= 1;
        }
        Console.Write("  /etc/opt  ");
        last_letter -= 12;
        while (last_letter > 1)
        {
            Console.Write('\u2550');
            last_letter -= 1;
        }
        Console.Write('\u2557');
        
      



    }
    static void CollumNames(int width, int count_column)
    {
        int last_letter;
        Console.BackgroundColor = ConsoleColor.Blue;

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write('\u2551');

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("etc:  Имя");
        Console.ForegroundColor = ConsoleColor.Cyan;

        last_letter = width - 11;
       
        while (last_letter > 0)
        {

            if (last_letter == 32 || last_letter == 16)
            {   Console.Write('\u2502');
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



        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write('\u2551');

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("etc:  Имя");
        Console.ForegroundColor = ConsoleColor.Cyan;

        last_letter = width - 11;
       
        while (last_letter > 0)
        {

            if (last_letter == 36)
            {
                Console.Write('\u2502');
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("  Размер");

                Console.ForegroundColor = ConsoleColor.Cyan;

                last_letter -= 9;
            }
            else if (last_letter == 24)
            {
                Console.Write('\u2502');
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("   Дата");

                Console.ForegroundColor = ConsoleColor.Cyan;

                last_letter -= 8;
            }
            else if (last_letter == 12)
            {
                 Console.Write('\u2502');
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("   Время");

                Console.ForegroundColor = ConsoleColor.Cyan;

                last_letter -= 9;
            }
            else
            {
                Console.Write(" ");
                last_letter--;
            }


        }
        Console.Write('\u2551');
        Console.ResetColor(); 
        Console.WriteLine();

    }
    static void DrawSmallBorder(int width)
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.Cyan;
        int last_letter;
        Console.Write('\u2551');
        last_letter = width - 2;
        while (last_letter > 0)
        {
            Console.Write('\u2500');
            last_letter--;
        }
        Console.Write('\u2551');
        

        

    }
    
    static void DrawBorder(int width)
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.Cyan;
        int last_letter;
        Console.Write('\u255A');
        last_letter = width - 2;
        while (last_letter > 0)
        {
            Console.Write('\u2550');
            last_letter--;
        }
        Console.Write('\u255D');




    }
    static void FileInfo(int width, FileItem file)
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.Cyan;
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


        Console.Write(ShortenName(file.ModificationDate.ToString("HH:mm"), 5));
        last_letter -= ShortenName(file.ModificationDate.ToString("HH:mm"), 5).Length;


        while (last_letter > 0)
        {
            Console.Write(" ");
            last_letter--;
        }
        Console.Write('\u2551');


        

    }
    static void Fields(int width, int height, int count_column, List <FileItem> items)
    {
        int last_letter, column_len, i = 0, j = 0;
        FileItem target = items[0];   
        width /= 2;
        column_len = (width - 2) / count_column;        
        CollumNames(width, count_column);
        


        while (height > 7 && i < items.Count )
        {
            //==========Левое поле ============




            FileItem file = items[i];
            i++;

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write('\u2551');
            last_letter = width - 2;
            j = 0;
            while (last_letter > 0)
            {
                if (j < file.Name.Length)
                {
                    Console.Write(file.Name[j]);
                    last_letter--;
                    j++;
                }
                else if (last_letter == 32 || last_letter == 16)
                {
                    Console.Write('\u2502');
                    j = 0;
                    last_letter--;
                    file = items[i++];
                    file.Name = ShortenName(file.Name, 10);
                }
                else if (j >= file.Name.Length)
                {
                    Console.Write(" ");
                    last_letter--;
                }



            }
            Console.Write('\u2551');



            //======Правое поле=========


    
           
            Console.Write('\u2551');
            last_letter = width - 2;

     
            Console.Write(ShortenName(file.Name, 10));
            last_letter -= ShortenName(file.Name, 10).Length;


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

            
            Console.Write(ShortenName(file.IsDirectory ? "<DIR>" : file.Size.ToString(), 8));
            last_letter -= ShortenName(file.IsDirectory ? "<DIR>" : file.Size.ToString(), 8).Length;


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
            //конец правого поля

            Console.ResetColor(); 
            Console.WriteLine();
            height--;



        }

        DrawSmallBorder(width);
        DrawSmallBorder(width);

        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine();

        FileInfo(width, target);
        FileInfo(width, target);

        Console.BackgroundColor =ConsoleColor.Black;
        Console.WriteLine();
        
        DrawBorder(width);
        DrawBorder(width);



    }
    
  
    static void LastInterface(int width)
    {
        int last_letter = width;
        Console.BackgroundColor = ConsoleColor.Black;
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
            last_letter-=3;
        }

    }
    static void InputField()
    {
        Console.ResetColor();
        Console.Write("[user opt]$");
 
    }


    static void Main()
    {
        int height = 25, width = 100;

        Console.Clear();
        List<FileItem> items = new List<FileItem>
        {
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

        FirstInterface(width);


        FirstLinefield(width);
        FirstLinefield(width);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine();

        Fields(width, height, 3, items);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine();

        Console.BackgroundColor = ConsoleColor.Black;
        InputField();
        Console.WriteLine();
        
        LastInterface(width);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine();







    }
}
