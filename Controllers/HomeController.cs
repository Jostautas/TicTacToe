using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    public class HomeController : Controller
    {
        static List<ButtonModel> buttons =  new List<ButtonModel>();

        private static int move = 0;

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
            int buttonValue = Int32.Parse(user);

            move++;

            if(buttons[buttonValue].State == '_')
            {
                buttons[buttonValue].State = 'X';

                if ( check('X') )
                {
                    move = 0;
                    return View("win", buttons);
                }
                else if (move <= 4)
                {
                    Computer();

                    if ( check('O') )
                    {
                        move = 0;
                        return View("lost", buttons);
                    }
                }
                else if (move > 4)
                {
                    move = 0;
                    return View("draw", buttons);
                }

            }
            return View("Index", buttons);
        }

        private void Computer()
        {
            Random random = new Random();

            while (true)
            {
                int x = random.Next(0, 8);
                if (buttons[x].State == '_')
                {
                    buttons[x].State = 'O';
                    return;
                }
            }
        }

        private bool check(char c)
        {
            if(
                    (buttons[0].State == c && buttons[1].State == c && buttons[2].State == c) ||  // 1st row
                    (buttons[3].State == c && buttons[4].State == c && buttons[5].State == c) ||  // 2nd row
                    (buttons[6].State == c && buttons[7].State == c && buttons[8].State == c) ||  // 3rd row

                    (buttons[0].State == c && buttons[3].State == c && buttons[6].State == c) ||  // 1st collumn
                    (buttons[1].State == c && buttons[4].State == c && buttons[7].State == c) ||  // 2nd collumn
                    (buttons[2].State == c && buttons[5].State == c && buttons[8].State == c) ||  // 3rd collumn

                    (buttons[0].State == c && buttons[4].State == c && buttons[8].State == c) ||  // right diagonal
                    (buttons[2].State == c && buttons[4].State == c && buttons[6].State == c)     // left diagonal
                )
            {
                return true;
            }
            return false;
        }

        public IActionResult HandleRestart()
        {
            clear();
            return View("Index", buttons);
        }

        private void clear()
        {
            for(int i = 0; i < 9; i++)
            {
                buttons[i].State = '_';
            }
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