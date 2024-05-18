using System.ComponentModel.DataAnnotations;

namespace shop.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public int ZipCode { get; set; }
    }
}
