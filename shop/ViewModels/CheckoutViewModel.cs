using shop.Models;
using System.ComponentModel.DataAnnotations;

namespace shop.ViewModels
{
    public class CheckoutViewModel
    {
        public List<GameAndQuantity> GamesAndQuantities { get; set; }
        public string? PromoCode { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Display(Name = "Zip")]
        [Required(ErrorMessage = "Zip is required")]
        public string Zip { get; set; }

        public bool SameAddress { get; set; }
        public bool SaveInfo { get; set; }
        public string PaymentMethod { get; set; }

        [Display(Name = "Name on card")]
        [Required(ErrorMessage = "Name on card is required")]
        public string CCName { get; set; }

        [Display(Name = "Credit card number")]
        [Required(ErrorMessage = "Credit card number is required")]
        public string CCNumber { get; set; }

        [Display(Name = "Expiration")]
        [Required(ErrorMessage = "Expiration is required")]
        public string CCExpiration { get; set; }

        [Display(Name = "CVV")]
        [Required(ErrorMessage = "CVV is required")]
        public string CCCVV { get; set; }
    }
}
