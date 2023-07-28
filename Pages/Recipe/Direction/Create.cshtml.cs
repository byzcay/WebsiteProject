using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipe.Direction
{
    public class CreateModel : PageModel
    {
        private readonly RecipeApp.Models.RecipeAppContext _context;

        public CreateModel(RecipeApp.Models.RecipeAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Recipe Recipe { get; private set; }

        [BindProperty(SupportsGet = true)]
        public int id { get; set; }


        public async Task<IActionResult> OnGetAysn(int id)
        {
            Recipe = await _context.Recipes.FindAsync(id);
            return Page();
        }


        [BindProperty]
        public int StepNumber { get; set; }
        [BindProperty]
        public string Description { get; set; }


        public Models.Direction Direction { get; set; } = null;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            Direction = new Models.Direction { RecipeId = id };

            Direction.Description = Description;
            Direction.StepNumber = StepNumber;

            if (!ModelState.IsValid || _context.Directions == null || Direction == null)
            {
                return Page();
            }



            _context.Directions.Add(Direction);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Recipe/Index");
        }

    }
}
