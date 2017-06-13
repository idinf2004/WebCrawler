using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawler
{
    public class CsvConvertor : IConvertor<string>
    {

        public string Convert(List<Dictionary<string, string>> results)
        {
            if (!results.Any()) return string.Empty;
            StringBuilder writer = new StringBuilder();
            // Generating Header.
            List<string> headers = results[0].Keys.Select(x => x).OrderBy(x => x).ToList();
            writer.AppendLine(string.Join(", ", headers.Select(h => h)));
            // Generating content.
            foreach (var item in results)
                writer.AppendLine(string.Join(", ", headers.Select(h => item[h])));
            return writer.ToString();
        }

    }
}