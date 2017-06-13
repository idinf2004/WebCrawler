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
    //Integration Test
    public class ConnectorTestsIT
    {
        private readonly IConnector<WebRequestConfig> _connector;
        private readonly IResponseParser _yahooFinParser;

        public ConnectorTestsIT()
        {
            
             _yahooFinParser = new YahooFinParser();
            WebRequestFactory webRequestFactory = new WebRequestFactory();
            _connector = new Connector(webRequestFactory);
        }

        [TestMethod()]
        public void RunTest()
        {
            WebRequestConfig webRequestConfig = new WebRequestConfig { Url = "https://finance.yahoo.com/world-indices" };
            _connector.Run(webRequestConfig, _yahooFinParser);

            Assert.AreEqual(State.Complete, _connector.GetState());
            Assert.IsTrue(_connector.GetResults().Count > 0);
            Assert.IsTrue(_connector.GetErrors().Count == 0);
        }

        [TestMethod()]
        public void RunWithNotFoundExceptionTest()
        {
            WebRequestConfig webRequestConfig = new WebRequestConfig { Url = "https://finance.yahoo.com/world-indice" };
            _connector.Run(webRequestConfig, _yahooFinParser);

            Assert.AreEqual(State.Failed, _connector.GetState());
            Assert.IsTrue(_connector.GetErrors().Count > 0);
            Assert.AreEqual(_connector.GetErrors().First().Key, "NotFound");
            Assert.IsTrue(_connector.GetResults().Count == 0);
        }
    }
}