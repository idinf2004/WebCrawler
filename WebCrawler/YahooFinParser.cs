using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebCrawler
{
    public class YahooFinParser : IResponseParser
    {
        public List<Dictionary<string, string>> Parse(string response)
        {
            List<Dictionary<string, string>> resultList = new List<Dictionary<string, string>>();
            Regex regex = new Regex(
                @"""exchange"":""(?<exchange>(.*?))""(.*?)" +
                @"""regularMarketTime"":{""raw"":(?<regularMarketTime>(.*?)),(.*?)""(.*?)" +
                @"""exchangeTimezoneShortName"":""(?<exchangeTimezoneShortName>(.*?))""(.*?)" +
                @"""regularMarketPrice"":{""raw"":(?<regularMarketPrice>(.*?)),(.*?)""(.*?)" +
                @"(""regularMarketVolume"":{""raw"":(?<regularMarketVolume>(.*?)),(.*?)""(.*?))?" +
                @"""marketState"":""(?<marketState>(.*?))""(.*?)" +
                @"""symbol"":""(?<symbol>(.*?))""(.*?)" +
                @"(""uuid"":""(?<uuid>(.*?))""(.*?))?" +
                @"""regularMarketChangePercent"":{""raw"":(?<regularMarketChangePercent>(.*?)),(.*?)""(.*?)" +
                @"""fullExchangeName"":""(?<fullExchangeName>(.*?))"""
                , RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(response);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    Dictionary<string, string> result = new Dictionary<string, string>
                    {
                        {"Id", match.Groups["uuid"].Value.Trim()},
                        {"Symbol", match.Groups["symbol"].Value.Trim()},
                        {"Exchange", match.Groups["exchange"].Value.Trim()},
                        {"ExchangeFullName", match.Groups["fullExchangeName"].Value.Trim()},
                        {"ExchangeTimeZone", match.Groups["exchangeTimezoneShortName"].Value.Trim()},
                        {"RegularMarketPrice", match.Groups["regularMarketPrice"].Value.Trim()},
                        {"RegularMarketVolume", match.Groups["regularMarketVolume"].Value.Trim()},
                        {"RegularMarketChangePercent", match.Groups["regularMarketChangePercent"].Value.Trim()},
                        {"RegularMarketTime", match.Groups["regularMarketTime"].Value.Trim()},
                        {"MarketState", match.Groups["marketState"].Value.Trim()}
                    };
                    resultList.Add(result);
                }
            }
            return resultList;
        }
    }
}