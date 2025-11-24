using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pardut_Daniela_Laborator2.Data;
using Pardut_Daniela_Laborator2.Models;
using Pardut_Daniela_Laborator2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardut_Daniela_Laborator2.Pages.Categories
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly Pardut_Daniela_Laborator2.Data.Pardut_Daniela_Laborator2Context _context;

        public IndexModel(Pardut_Daniela_Laborator2.Data.Pardut_Daniela_Laborator2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;
        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }


        public async Task OnGetAsync(int? id)
        {
            CategoryData = new CategoryIndexData();


            CategoryData.Categories = await _context.Category
                .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.Author)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();


            if (id != null)
            {
                CategoryID = id.Value;

                var category = CategoryData.Categories
                    .Where(c => c.ID == id.Value)
                    .Single();

                CategoryData.Books = category.BookCategories
                    .Select(bc => bc.Book);
            }
        }

    }
}
