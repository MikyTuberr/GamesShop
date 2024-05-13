﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Models
{
    public class Game
    {
        [Key]
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required bool IsOnSale { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public required decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PriceAfterSale { get; set; }
        public required string ImagePath { get; set; }
        public required string Alt {  get; set; }
    }
}