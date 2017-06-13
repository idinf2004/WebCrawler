using System.Collections.Generic;

namespace WebCrawler
{
    public interface IResponseParser
    {
        List<Dictionary<string, string>> Parse(string response);
    }
}