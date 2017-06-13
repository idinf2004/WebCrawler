using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace WebCrawler.Tests
{
    [TestClass()]
    public class ConnectorTests
    {
        private IConnector<WebRequestConfig> _connector;
        private WebRequestConfig _webRequestConfig;
        private Mock<IResponseParser> _parser;

        public ConnectorTests()
        {

            var webRequestFactory = new Mock<WebRequestFactory>();
             _connector = new Connector(webRequestFactory.Object);

           
            Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
            dictionary1.Add("name", "Aidin");
            dictionary1.Add("family", "firouzabady");
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            dictionary2.Add("name", "Joe");
            dictionary2.Add("family", "Way");
            Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
            dictionary3.Add("name", "Chris");
            dictionary3.Add("family", "Mile");
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            results.Add(dictionary1);
            results.Add(dictionary2);
            results.Add(dictionary3);

            _webRequestConfig =  new WebRequestConfig { Url = "https://finance.yahoo.com/world-indices" };
            _parser = new Mock<IResponseParser>();
            _parser.Setup(x => x.Parse(It.IsAny<string>())).Returns(results);

        }

        [TestMethod()]
        public void GetResultsTest()
        {
            _connector.Run(_webRequestConfig, _parser.Object);
            Assert.AreEqual(3,_connector.GetResults().Count);
            Assert.AreEqual(State.Complete, _connector.GetState());
        }
        
        [TestMethod()]
        public void GetErrorsTest()
        {
            _webRequestConfig.Url = "badUrl";
            _connector.Run(_webRequestConfig, _parser.Object);
            Assert.AreEqual(1,_connector.GetErrors().Count);
            Assert.AreEqual(State.Failed, _connector.GetState());
        }

        [TestMethod()]
        public void GetStartStateTest()
        {
            Assert.AreEqual(State.Init, _connector.GetState());
        }

    }
}