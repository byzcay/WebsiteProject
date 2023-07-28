using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;
using System.Security.Claims;

namespace RecipeApp.Areas.Identity.Pages.Account.Manage
{
    public class FavouriteRecipesModel : PageModel
    {
        private readonly RecipeAppContext _context;

        public FavouriteRecipesModel(RecipeAppContext context)
        {
            _context = context;
        }
        public IList<Favori2> Favori2 { get; set; }
        public IList<Recipe> Recipe { get; set; }
        [BindProperty]
        public string userId { get; set; }
        public List<double> RecipeAverageRatingsList { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

            

            public async Task CalculateRatings()
            {
                var AllRecipes = await (from re in _context.Recipes
                                        select re).ToListAsync();
                var AllRatings = await (from ra in _context.Ratings
                                        select ra).ToListAsync();

                RecipeAverageRatingsList = new List<Double>();

            }

        public async Task OnGetAsync()
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Load the Favori2 and Recipe collections
            Favori2 = await _context.Favori2s.ToListAsync();
            Recipe = await _context.Recipes.ToListAsync();
        }


    }
}



