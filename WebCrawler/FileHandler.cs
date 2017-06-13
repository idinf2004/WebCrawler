using System;
using System.IO;

namespace WebCrawler
{
    public class FileHandler
    {
        public string GetFilePath { get; private set; }
        public void Save(string fileExtention ,string input, string filePath = null)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Outputs\";
            }
            string fileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            filePath = filePath + fileName + "." + fileExtention;
            System.IO.File.WriteAllText(filePath, input);
            GetFilePath = filePath;
        }
    }
}