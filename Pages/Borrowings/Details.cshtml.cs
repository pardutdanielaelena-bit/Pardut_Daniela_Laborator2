using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pardut_Daniela_Laborator2.Data;
using Pardut_Daniela_Laborator2.Models;

namespace Pardut_Daniela_Laborator2.Pages.Borrowings
{
    public class DetailsModel : PageModel
    {
        private readonly Pardut_Daniela_Laborator2.Data.Pardut_Daniela_Laborator2Context _context;

        public DetailsModel(Pardut_Daniela_Laborator2.Data.Pardut_Daniela_Laborator2Context context)
        {
            _context = context;
        }

        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Borrowing = await _context.Borrowing
            .Include(b => b.Member)
            .Include(b => b.Book)
                .ThenInclude(b => b.Author)
            .FirstOrDefaultAsync(m => m.ID == id);

            if (Borrowing == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
