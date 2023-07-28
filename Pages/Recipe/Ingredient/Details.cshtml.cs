using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipe.Ingredient
{
    public class DetailsModel : PageModel
    {
        private readonly RecipeApp.Models.RecipeAppContext _context;
        public Models.Recipe Recipe { get; set; } = default!;

        public DetailsModel(RecipeApp.Models.RecipeAppContext context)
        {
            _context = context;
        }

      public Models.Ingredient Ingredient { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Ingredients == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null)
            {
                return NotFound();
            }
            else 
            {
                Ingredient = ingredient;
            }
            return Page();
        }
    }
}
