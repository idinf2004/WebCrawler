using System.Collections.Generic;

namespace WebCrawler
{
    public interface IConvertor<out TOut>
    {
        TOut Convert(List<Dictionary<string, string>> results);
    }
}
