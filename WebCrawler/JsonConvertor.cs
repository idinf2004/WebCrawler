using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace WebCrawler
{
    public class JsonConvertor : IConvertor<string>
    {
        public string Convert(List<Dictionary<string, string>> results)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            return jsonSerialiser.Serialize(results);
           
        }
    }
}