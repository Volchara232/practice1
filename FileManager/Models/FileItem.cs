using System;

namespace FileManager.Models
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
}