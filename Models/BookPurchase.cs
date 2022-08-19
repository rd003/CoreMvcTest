using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcTest.Models
{
    public class BookPurchase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name should not exceed 50 characters")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please select mobile number")]
        [MaxLength(10, ErrorMessage = "MobileNumber should not exceed 10 characters")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "PurchaseDate is required")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "please select a book")]
        public int Book_Id { get; set; }

        public string BookName { get; set; }
        public string AuthorAndPublisher { get; set; }

        public IEnumerable<SelectListItem> BookList { get; set; }
    }
}
