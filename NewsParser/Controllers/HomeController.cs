using Microsoft.AspNetCore.Mvc;
using NewsParser.Models;
using NewsParser.Services;
using System.Diagnostics;

namespace NewsParser.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ArticleService _articleService;
        private readonly TelegramService _telegramService;
        private Timer _timer;
        object locker = new();
        public IParser UkrNetParser { get; set; }

        public HomeController(ILogger<HomeController> logger, ArticleService articleService, TelegramService telegramService)
        {
            _logger = logger;
            _articleService= articleService;
            _telegramService= telegramService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            _timer = new Timer(new TimerCallback(ParseUkrNetNews), null, 0, 600000);

            return View();
        }

        private void ParseUkrNetNews(object obj)
        {
            lock (locker)
            {
                var parser = new UkrNetParser("https://www.city.kharkov.ua/", _articleService, _telegramService);
                var isLoaded = parser.LoadDocument();
                if (isLoaded)
                {
                    parser.Parse();
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}