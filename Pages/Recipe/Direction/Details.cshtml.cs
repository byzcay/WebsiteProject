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
    public class DetailsModel : PageModel
    {
        private readonly RecipeApp.Models.RecipeAppContext _context;

        public DetailsModel(RecipeApp.Models.RecipeAppContext context)
        {
            _context = context;
        }

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
    }
}
