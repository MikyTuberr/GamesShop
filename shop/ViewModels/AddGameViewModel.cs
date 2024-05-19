using System.ComponentModel.DataAnnotations;

namespace shop.ViewModels
{
    public class AddGameViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Game Name")]
        [Required(ErrorMessage = "Game name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }

        [Display(Name = "Is On Sale")]
        [Required(ErrorMessage = "Sale status is required")]
        public bool IsOnSale { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01f, 999999.99f, ErrorMessage = "Price must be between 0.01 and 999999.99")]
        public decimal Price { get; set; }

        [Display(Name = "Price After Sale")]
        [Range(0.01f, 999999.99f, ErrorMessage = "Price after sale must be between 0.01 and 999999.99")]
        public decimal? PriceAfterSale { get; set; }

        [Display(Name = "Image Path")]
        [Required(ErrorMessage = "Image path is required")]
        public string ImagePath { get; set; }

        [Display(Name = "Alt Text")]
        [Required(ErrorMessage = "Alt text is required")]
        public string Alt { get; set; }
    }
}
