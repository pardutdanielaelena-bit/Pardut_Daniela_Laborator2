using System.ComponentModel.DataAnnotations;

namespace Pardut_Daniela_Laborator2.Models
{
    public class Author
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // Proprietate calculată pentru afișare completa
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        // Navigation property - un autor poate avea multiple carti
        public ICollection<Book>? Books { get; set; }
    }
}
