using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCrawler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Tests
{
    [TestClass()]
    public class FileHandlerTests
    {
        private FileHandler _fileHandler;
        public FileHandlerTests()
        {
            _fileHandler = new FileHandler();
        }

        [TestMethod()]
        public void SaveTest()
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\";
            _fileHandler.Save("txt","test", path);
            bool expected = File.Exists(_fileHandler.GetFilePath);
            Assert.IsTrue(expected);
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (File.Exists(_fileHandler.GetFilePath))
            {
                File.Delete(_fileHandler.GetFilePath);
            }
        }

    }
}