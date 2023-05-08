using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace TestZadanie4.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }

        public List<Book>? Books { get; set; }
     
        public int CountBooks { get; set; }
  
    }

}

    

