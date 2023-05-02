using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SpeedGame.Pages
{
    public class GameModel : PageModel
    {
        private readonly ILogger<GameModel> _logger;

        public GameModel(ILogger<GameModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}