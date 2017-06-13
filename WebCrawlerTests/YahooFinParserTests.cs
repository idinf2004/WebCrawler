using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCrawler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Tests
{
    [TestClass()]
    public class YahooFinParserTests
    {
        private readonly IResponseParser _parser;
        private readonly string _response;
        public YahooFinParserTests()
        {
            _parser = new YahooFinParser();
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\output.html";
            // Open the file to read from.
            _response = File.ReadAllText(path);
        }

        [TestMethod()]
        public void ParseTest()
        {
            var actual = _parser.Parse(_response);
            Assert.AreEqual(actual.Count,42);
        }

        
    }
}