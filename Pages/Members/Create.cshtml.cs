using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pardut_Daniela_Laborator2.Data;
using Pardut_Daniela_Laborator2.Models;

namespace Pardut_Daniela_Laborator2.Pages.Members
{
    public class CreateModel : PageModel
    {
        private readonly Pardut_Daniela_Laborator2.Data.Pardut_Daniela_Laborator2Context _context;

        public CreateModel(Pardut_Daniela_Laborator2.Data.Pardut_Daniela_Laborator2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Member Member { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Member.Add(Member);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
