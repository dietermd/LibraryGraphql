using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryGraphqlApi.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        public DateTime? DOB { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
