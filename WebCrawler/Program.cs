using System;
using System.Collections.Generic;
using System.Configuration;

namespace WebCrawler
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            var formatOption = string.Empty;
            var formats = new Dictionary<string, string> {{"1", "csv"}, {"2", "json"}};
            while (!formats.ContainsKey(formatOption))
            {
                formatOption = Menu();
            }

            WebRequestFactory webRequestFactory = new WebRequestFactory();
            IConnector<WebRequestConfig> connector = new Connector(webRequestFactory);

            WebRequestConfig webRequestConfig = new WebRequestConfig { Url = "https://finance.yahoo.com/world-indices" };
            IResponseParser yahooFinParser = new YahooFinParser();
            Console.WriteLine("Connector State: " + connector.GetState());
            connector.Run(webRequestConfig, yahooFinParser);
            if (connector.GetState() == State.Complete)
            {
                Console.WriteLine("Connector State: " + connector.GetState());
                FileHandler fileHandlerHandler = new FileHandler();
                string outputPath = ConfigurationManager.AppSettings["OutputFilePath"];
                if (formatOption == "2")
                {
                    // save json file
                    IConvertor<string> jsonConvertor = new JsonConvertor();
                    string output = jsonConvertor.Convert(connector.GetResults());
                    fileHandlerHandler.Save(formats[formatOption], output, outputPath);                   
                }

                if (formatOption == "1")
                {
                    // save CSV file
                    IConvertor<string> csvConvertor = new CsvConvertor();
                    string output = csvConvertor.Convert(connector.GetResults());
                    fileHandlerHandler.Save(formats[formatOption], output, outputPath);
                }
                Console.WriteLine("The results has been saved in the following path: " + fileHandlerHandler.GetFilePath);
                Console.ReadLine();
            }
            else if(connector.GetErrors().Count>0)
            {
                foreach (var error in connector.GetErrors())
                {
                    Console.WriteLine("Connector State: " + connector.GetState());
                    Console.WriteLine(error.Key +" : " +error.Value);
                }
            }
        }


        private static string Menu()
        {
            Console.WriteLine("1. CSV Format");
            Console.WriteLine("2. Json Format");
            Console.WriteLine("Please select one of the above output formats (1 or 2)");
            return Console.ReadLine();
        }



    }
}