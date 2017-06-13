using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WebCrawler
{
    public class Connector : IConnector<WebRequestConfig>
    {
        private readonly WebRequestFactory _webRequestFactory;
        private List<Dictionary<string, string>> _resultList;
        private State _state;
        private readonly Dictionary<string, string> _errors;
        private const string DefaultContentType = "application/x-www-form-urlencoded";

        public Connector(WebRequestFactory webRequestFactory)
        {
            _webRequestFactory = webRequestFactory;
            _resultList = new List<Dictionary<string, string>>();
            _errors = new Dictionary<string, string>();
            _state = State.Init;
        }

        public List<Dictionary<string, string>> GetResults()
        {
            return _resultList;
        }

        public Dictionary<string, string> GetErrors()
        {
            return _errors;
        }


        public State GetState()
        {
            return _state;
        }

        public void Run(WebRequestConfig webRequestConfig, IResponseParser parser)
        {
            try
            {
                _state = State.Start;
                var request = _webRequestFactory.Create(webRequestConfig.Url);
                var requestMethod = webRequestConfig?.Method?.ToUpper();
                if (webRequestConfig.Headers?.Count > 0)
                {
                    request.Headers = new WebHeaderCollection();
                    webRequestConfig.Headers.ForEach(x => request.Headers.Add(x.Key, x.Value));
                }
                if (requestMethod == "POST")
                {
                    if (webRequestConfig.PostBody == null)
                    {
                        webRequestConfig.PostBody = "";
                    }
                    request.Method = requestMethod;
                    request.ContentType = webRequestConfig.ContentType?? DefaultContentType;

                        var data = Encoding.ASCII.GetBytes(webRequestConfig.PostBody);
                        request.ContentLength = data.Length;
                        using (var stream = request.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                }

                var response = (HttpWebResponse) request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                _resultList = parser.Parse(responseString);
                _state = State.Complete;
            }
            catch (WebException ex)
            {
                _state = State.Failed;
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    _errors.Add(response?.StatusCode.ToString() ?? ex.GetType().FullName, ex.Message);
                }
                else
                {
                    _errors.Add(ex.GetType().FullName, ex.Message);
                }
            }
            catch (Exception ex)
            {
                _state = State.Failed;
                _errors.Add(ex.GetType().FullName, ex.Message);
            }
        }

    }
}