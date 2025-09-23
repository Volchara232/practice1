using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.UI
{
    public abstract class FieldLine
    {
        protected int Width { get; }
        protected List<FileItem> Items { get; }

        protected FieldLine(int width, List<FileItem> items)
        {
            Width = width;
            Items = items;
        }

        public abstract void DrawDescription();
        public abstract int DrawData(int startIndex, int targetId, bool isTargetField);
        
        public static string ShortName(string name, int maxLength)
        {
            int i = name.Length - 1;
            bool hasExt = false;
            string tmp, ext = "", extReverse = "";
            
            if (name.Length > maxLength)
            {
                while (i >= 0)
                {
                    if (name[i] == '.')
                    {
                        hasExt = true;
                        break;
                    }
                    extReverse += name[i];
                    i--;
                }
                
                if (hasExt)
                {
                    for (i = extReverse.Length - 1; i >= 0; i--)
                    {
                        ext += extReverse[i];
                    }
                    tmp = $"{name.Substring(0, maxLength - ext.Length - 2)}~.{ext}";
                }
                else
                {
                    tmp = $"{name.Substring(0, maxLength - 1)}~";
                }
                return tmp;
            }
            return name;
        }
    }
}