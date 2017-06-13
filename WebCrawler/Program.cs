using System;
using System.Collections.Generic;

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
            connector.Run(webRequestConfig, yahooFinParser);
            if (connector.GetState() == State.Complete)
            {      
                FileHandler fileHandlerHandler = new FileHandler();
                if (formatOption == "2")
                {
                    // save json file
                    IConvertor<string> jsonConvertor = new JsonConvertor();
                    string output = jsonConvertor.Convert(connector.GetResults());
                    fileHandlerHandler.Save(formats[formatOption], output);                   
                }

                if (formatOption == "1")
                {
                    // save CSV file
                    IConvertor<string> csvConvertor = new CsvConvertor();
                    string output = csvConvertor.Convert(connector.GetResults());
                    fileHandlerHandler.Save(formats[formatOption], output);
                }
                Console.WriteLine("The results has been saved in the following path: " + fileHandlerHandler.GetFilePath);
            }
            else if(connector.GetErrors().Count>0)
            {
                foreach (var error in connector.GetErrors())
                {
                    Console.WriteLine(error.Key +" : " +error.Value);
                }
            }
        }


        private static string Menu()
        {
            Console.WriteLine("1. CSV Format");
            Console.WriteLine("2. Json Format");
            Console.WriteLine("Please select one  the above output formats (1 or 2)");
            return Console.ReadLine();
        }



    }
}