using Microsoft.AspNetCore.Mvc;
using shop.data;
using shop.Models;
using System.Diagnostics;

namespace shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Game> games = _context.Games.ToList();
            return View(games);
        }

        public IActionResult Details(string id)
        {
            Game game = _context.Games.FirstOrDefault(g => g.Id == id);
            List<Game> games = _context.Games.ToList();
            ViewData["Game"] = game;
            ViewData["Games"] = games;
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
