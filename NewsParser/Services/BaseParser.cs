using HtmlAgilityPack;

namespace NewsParser.Services
{
    public abstract class BaseParser : IParser
    {
        protected readonly string _url;
        protected string _content;
        protected readonly ArticleService _articleService;
        protected object locker = new();
        public BaseParser(string url, ArticleService articleService)
        {
            _url = url;
            _articleService = articleService;
        }
        public virtual bool LoadDocument()
        {
            lock (locker)
            {
                var html = LoadHtml(_url).Result;
                _content = html;
                return !string.IsNullOrEmpty(html);
            }
        }

        public virtual void Parse()
        {
            lock (locker)
            {
                var document = new HtmlDocument();
                document.LoadHtml(_content);
                ParseNewsLinks(document);
            }
        }

        protected abstract List<string> ParseNewsLinks(HtmlDocument document);

        protected virtual async Task<string> LoadHtml(string url)
        {
            var result = string.Empty;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }

            return result;
        }
    }
}
