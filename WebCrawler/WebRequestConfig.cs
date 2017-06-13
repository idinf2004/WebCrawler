using System.Collections.Generic;

namespace WebCrawler
{
    public class WebRequestConfig
    {
        public string Method { get; set; }
        public string Url { get; set; }
        public string ContentType { get; set; }
        public string PostBody { get; set; }
        public List<KeyValuePair<string, string>> Headers { get; set; }

    }
}