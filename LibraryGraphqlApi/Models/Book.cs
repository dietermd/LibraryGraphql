using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryGraphqlApi.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [MaxLength(100)]
        [Required]
        public string Publisher { get; set; }

        public ICollection<Author> Authors { get; set; }
    }
}
