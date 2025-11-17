namespace Pardut_Daniela_Laborator2.Models
{
    public class BookCategory
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; } = default!;
        public int CategoryID { get; set; }
        public Category Category { get; set; } = default!;
    }
}
