using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using NewsParserDAL.Models;
using System.Formats.Asn1;
using System.Globalization;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace NewsParser.Services
{
    public class UkrNetParser : BaseParser
    {
        private TelegramService _telegramService;
        public UkrNetParser(string url, ArticleService articleService, TelegramService telegramService) : base(url, articleService)
        {
            _telegramService = telegramService;
        }

        private async Task ParseNewsPage(string url)
        {
            var html = LoadHtml(url).Result;
            var newsDoc = new HtmlDocument();
            newsDoc.LoadHtml(html);
            var categoryElement = newsDoc.DocumentNode.SelectSingleNode(@"//div[starts-with(@class, 'block_post')]/div[@class='data_post']/a");
            if (categoryElement != null)
            {
                var categoryName = categoryElement.InnerText;
                var category = await _articleService.CreateIfNotExistAndGetCategoryByNameAsync(categoryName);
                var article = new Article();
                article.Url = url;
                var dateElement = newsDoc.DocumentNode.SelectSingleNode(@"//div[starts-with(@class, 'block_post')]/div[@class='data_post']/em");
                article.DatePublished = dateElement == null ? DateTime.Now.ToShortTimeString() : dateElement.InnerText;
                article.DateSaved = DateTime.Now;
                var titleElement = newsDoc.DocumentNode.SelectSingleNode(@"//div[starts-with(@class, 'block_post')]/span[@class='heading_name']");
                article.Title = titleElement?.InnerText;
                if (_articleService.IsExist(article.Title))
                {
                    return;
                }
                var contentElements = newsDoc.DocumentNode.SelectNodes(@"//div[starts-with(@class, 'block_post')]/*[@class='justifyfull' or @class='heading']");
                if (contentElements != null)
                {
                    foreach (var item in contentElements)
                    {
                        var str = Regex.Replace(item.InnerText, "<.*?>", string.Empty);
                        article.Content += str.Replace(@"&nbsp;", string.Empty);
                    }
                }
                await _articleService.UpdateOrCreateArticleAsync(article, category);
                _telegramService.SendArticle(article);
            }

        }
        protected override List<string> ParseNewsLinks(HtmlDocument document)
        {
            var links = document.DocumentNode.SelectNodes(@"//div[starts-with(@class, 'list_news')]/ul/li/a").Select(x => x.Attributes["href"].Value).ToList();
            if (links != null)
            {
                foreach (var item in links)
                {
                    ParseNewsPage(item).Wait();
                }
            }
            return links;
        }
    }
}
