using shop.Models;

namespace shop.ViewModels
{
    public class CartViewModel
    {
        public required List<GameAndQuantity> GamesAndQuantities { get; set; }
    }
}
