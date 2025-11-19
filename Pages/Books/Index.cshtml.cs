using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pardut_Daniela_Laborator2.Data;
using Pardut_Daniela_Laborator2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Pardut_Daniela_Laborator2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Pardut_Daniela_Laborator2.Data.Pardut_Daniela_Laborator2Context _context;

        public IndexModel(Pardut_Daniela_Laborator2.Data.Pardut_Daniela_Laborator2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;
        public BookData BookD { get; set; }
        public int BookID { get; set; }
        public int CategoryID { get; set; }
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }

        public string CurrentFilter { get; set; }


        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            BookD = new BookData();

     
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";

            CurrentFilter = searchString;

            BookD.Books = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

             if (!String.IsNullOrEmpty(searchString))
            {
                BookD.Books = BookD.Books.Where(s => 
                    (s.Title != null && s.Title.ToLower().Contains(searchString.ToLower())) ||
                    (s.Author != null && s.Author.FirstName != null && s.Author.FirstName.ToLower().Contains(searchString.ToLower())) ||
                    (s.Author != null && s.Author.LastName != null && s.Author.LastName.ToLower().Contains(searchString.ToLower())) ||
                    (s.Author != null && s.Author.FullName != null && s.Author.FullName.ToLower().Contains(searchString.ToLower())));
            }

            if (id != null)
            {
                BookID = id.Value;
                Book book = BookD.Books
                    .Where(i => i.ID == id.Value).Single();
                BookD.Categories = book.BookCategories.Select(s => s.Category);
            }
            switch (sortOrder)
                {
                    case "title_desc":
                        BookD.Books = BookD.Books.OrderByDescending(s =>
                       s.Title);
                        break;
                    case "author_desc":
                        BookD.Books = BookD.Books.OrderByDescending(s =>
                       s.Author.FullName);
                        break;
                    case "author":
                        BookD.Books = BookD.Books.OrderBy(s =>
                       s.Author.FullName);
                        break;
                    default:
                        BookD.Books = BookD.Books.OrderBy(s => s.Title);
                        break;
                }
            }
        }
    }
