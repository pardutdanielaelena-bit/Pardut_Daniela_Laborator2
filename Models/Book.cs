using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Pardut_Daniela_Laborator2.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Display(Name = "Book Title")]// Modifică textul afișat în UI
        public string Title { get; set; }

        [Column(TypeName = "decimal(6, 2)")]// Permite valori cu două zecimale și o precizie totală de 6
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; } // Noua proprietate

        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; } // navigation property
        // Relație cu Author
        public int? AuthorID { get; set; }
        public Author? Author { get; set; }  // navigation property
    }
}
