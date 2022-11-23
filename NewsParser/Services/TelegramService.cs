using NewsParserDAL.Models;
using System.Net;
using System.Text;

namespace NewsParser.Services
{
    public class TelegramService
    {
        private object _lock = new object();
        public void SendArticle(Article article)
        {
            lock (_lock)
            {
                string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                string apiToken = "5982830866:AAH014tcl0nHGGyGFIEQekdr74i6fNSmQ-4";
                string chatId = "5056540466";
                string text = article.Title;
                urlString = String.Format(urlString, apiToken, chatId, text);
                WebRequest request = WebRequest.Create(urlString);
                Stream rs = request.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(rs);
                string line = "";
                StringBuilder sb = new StringBuilder();
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line != null)
                        sb.Append(line);
                }
                string response = sb.ToString();
                // Do what you want with response
            }
        }
    }
}
