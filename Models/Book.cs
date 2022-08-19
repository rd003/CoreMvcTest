using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcTest.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public int BookCategory_Id { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string PublisherName { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        public bool IsDeleted { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<SelectListItem> BookCategoryList { get; set; }
    }
}
