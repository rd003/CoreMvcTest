using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcTest.Models
{
    public class BookCategory
    {
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        //public object Books { get; set; }
        public bool IsDeleted { get; set; }

    }
}
