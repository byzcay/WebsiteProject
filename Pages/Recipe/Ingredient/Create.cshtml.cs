using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipe.Ingredient
{
    public class CreateModel : PageModel
    {
        private readonly RecipeApp.Models.RecipeAppContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CreateModel(RecipeApp.Models.RecipeAppContext context, 
                                UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Models.Recipe? Recipe { get; private set; }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            
            Recipe = await _context.Recipes.FindAsync(id);

            return Page();
        }

        [BindProperty]
        public int Quantity { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Unit { get; set; }

        [BindProperty]
        public int Calories { get; set; }

        public Models.Ingredient Ingredient { get; set; } = null!;
        
        public async Task<IActionResult> OnPostAsync()
        {

            Ingredient = new Models.Ingredient { RecipeId = id};
           
            Ingredient.Quantity = Quantity;
            Ingredient.Unit = Unit;
            Ingredient.Name = Name;
            Ingredient.Calories = Calories;

            _context.Ingredients.Add(Ingredient);
            await _context.SaveChangesAsync();


            return RedirectToPage("/Recipe/Index");

        }
    }
}
