using shop.Models;

namespace shop.ViewModels
{
    public class HomeDetailsViewModel
    {
        public required Game Game { get; set; }
        public required IEnumerable<Game> Games { get; set; }
    }
}
