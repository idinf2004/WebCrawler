using System.Collections.Generic;

namespace WebCrawler
{
    public interface IConnector<in TConfig>
    {
        State GetState();
        void Run(TConfig webRequestConfig, IResponseParser parser);
        List<Dictionary<string, string>> GetResults();

        Dictionary<string, string> GetErrors();
    }
}