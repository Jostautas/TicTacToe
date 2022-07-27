using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    public class HomeController : Controller
    {
        static List<ButtonModel> buttons =  new List<ButtonModel>();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            for(int i = 0; i < 9; i++)
            {
                buttons.Add(new ButtonModel('_'));
            }
            return View("Index", buttons);
        }

        public IActionResult HandleButtonClick(string user)
        {
            int buttonValue = Int32.Parse(user) - 1;
            if(buttons[buttonValue].State == '_')
            {
                buttons[buttonValue].State = 'X';
            }
            return View("Index", buttons);
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