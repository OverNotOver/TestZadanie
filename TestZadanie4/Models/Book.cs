using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestZadanie4.Models
{
    public class Book
    {
        
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public int NumberPages { get; set; }

        [NotMapped]
        public bool Checked { get; set; }

        public Author? Author { get; set; }



       
    }
}
