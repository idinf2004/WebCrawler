using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Tests
{
    [TestClass()]
    public class ConvertorTests
    {
        private List<Dictionary<string, string>> _list;

        [TestInitialize]
        public void Init()
        {
            Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
            dictionary1.Add("name", "Aidin");
            dictionary1.Add("family", "firouzabady");
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("name", "Joe");
            dictionary2.Add("family", "Way");
            Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
            dictionary3.Add("name", "Chris");
            dictionary3.Add("family", "Mile");
            _list = new List<Dictionary<string, string>>();
            _list.Add(dictionary1);
            _list.Add(dictionary2);
            _list.Add(dictionary3);

        }

        [TestMethod]
        public void CsvConvertTest()
        {
            var convertor = new CsvConvertor();
            string expected = "family, name\r\nfirouzabady, Aidin\r\nWay, Joe\r\nMile, Chris\r\n";
            string actual = convertor.Convert(_list);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void JsonConvertTest()
        {
            var convertor = new JsonConvertor();
            string expected =
                "[{\"name\":\"Aidin\",\"family\":\"firouzabady\"},{\"name\":\"Joe\",\"family\":\"Way\"},{\"name\":\"Chris\",\"family\":\"Mile\"}]";
            string actual = convertor.Convert(_list);
            Assert.AreEqual(expected, actual);
        }
    }
}