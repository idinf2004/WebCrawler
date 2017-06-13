using System.Net;

namespace WebCrawler
{
    /// <summary>
    /// just to be able to mock httpWebRequest
    /// </summary>
    public class WebRequestFactory
    {
        public HttpWebRequest Create(string uri)
        {
            return (HttpWebRequest)WebRequest.Create(uri);
        }

    }
}