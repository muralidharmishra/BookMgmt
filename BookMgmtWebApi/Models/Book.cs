using System.ComponentModel.DataAnnotations;

namespace BookMgmtWebApi.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Genre { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        public double? Price { get; set; }
    }
}
