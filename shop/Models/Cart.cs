using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Models
{
    public class Cart
    {
        [Key]
        public required string ClientId { get; set; }
        public List<GameAndQuantity> GamesAndQuantities { get; set; } = new List<GameAndQuantity>();
    }
}
