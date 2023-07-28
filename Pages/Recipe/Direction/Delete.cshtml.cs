using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipe.Direction
{
    public class DeleteModel : PageModel
    {
        private readonly RecipeApp.Models.RecipeAppContext _context;

        public DeleteModel(RecipeApp.Models.RecipeAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.Direction Direction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Directions == null)
            {
                return NotFound();
            }

            var direction = await _context.Directions.FirstOrDefaultAsync(m => m.Id == id);

            if (direction == null)
            {
                return NotFound();
            }
            else 
            {
                Direction = direction;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Directions == null)
            {
                return NotFound();
            }
            var direction = await _context.Directions.FindAsync(id);

            if (direction != null)
            {
                Direction = direction;
                _context.Directions.Remove(Direction);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
