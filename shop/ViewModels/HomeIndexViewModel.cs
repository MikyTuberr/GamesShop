using shop.Models;

namespace shop.ViewModels
{
    public class HomeIndexViewModel
    {
        public required IEnumerable<Game> Games { get; set; }
    }
}
