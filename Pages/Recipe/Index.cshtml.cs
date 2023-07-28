using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipe
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly RecipeApp.Models.RecipeAppContext _context;

        public IndexModel(RecipeApp.Models.RecipeAppContext context)
        {
            _context = context;
        }
        public IList<Models.Recipe> Recipe { get; set; } = default!;
        public IList<Models.Ingredient> Ingredient { get; set; } = default!;
        public List<double> RecipeAverageRatingsList { get; set; } = default!;
        public IList<Models.Favori2> Favori2 { get; set; } = default!;


        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Titles { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? RecipeTitle { get; set; }
        public string userId { get; set; }

       

        //[BindProperty(SupportsGet = true)]
        //public string? ingredientSearch { get; set; }
        public async Task CalculateRatings()
        {
            var AllRecipes = await (from re in _context.Recipes
                                    select re).ToListAsync();
            var AllRatings = await (from ra in _context.Ratings
                                    select ra).ToListAsync();

            RecipeAverageRatingsList = new List<Double>();


            foreach (var item in AllRecipes)
            {
                List<int?> reciperates = await (from x in _context.Ratings
                                                where x.RecipeId == item.Id
                                                select x.Value).ToListAsync();

                if (reciperates.Count >= 1)
                {
                    double avg = CalculateRate.GetRating(reciperates.Select(x => x.Value).ToList());
                    avg = Math.Round(avg, 1);
                    RecipeAverageRatingsList.Add(avg);
                }
                else
                {
                    RecipeAverageRatingsList.Add(0.0);
                }
            }

        }

        public async Task OnGetAsync(string SearchString)
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var recipe = from r in _context.Recipes
                         select r;
            //var ing = from m in _context.Ingredients
            //             select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                var recipeIdsWithMatchingIngredients = await _context.Ingredients
                   .Where(i => i.Name.Contains(SearchString))
                   .Select(i => i.RecipeId)
                   .ToListAsync();

                recipe = recipe.AsNoTracking().Where(recipe =>
                    recipe.Cuisine.Contains(SearchString) ||
                    recipe.Category.Contains(SearchString) ||
                    recipe.Title.Contains(SearchString) ||
                    recipeIdsWithMatchingIngredients.Contains(recipe.Id));

                //Ingredient = await _context.Ingredients.Where(i => i.IngredientName == SearchString).ToListAsync();

                //var recipeIds = Ingredient.Select(i => i.RecipeId).ToList();

                //Recipe = await _context.Recipes.Where(r => recipeIds.Contains(r.Id)).ToListAsync();

            }

            await CalculateRatings();

            var groupedCalories = _context.Ingredients
            .GroupBy(ingredient => ingredient.RecipeId)
            .Select(group => new { RecipeId = group.Key, TotalCalories = group.Sum(ingredient => ingredient.Calories) })
            .ToList();

            foreach (var calories in groupedCalories)
            {
                var recipes = await _context.Recipes.FindAsync(calories.RecipeId);
                if (recipes != null)
                {
                    recipes.TotalCalories = calories.TotalCalories;
                }
            }


            var Recipes = _context.Recipes.OrderByDescending(r => r.TotalCalories).ToList();

            Recipe = await recipe.ToListAsync();


            await _context.SaveChangesAsync();

        }


        [BindProperty(SupportsGet = true)]
        public int id { get; set; }


        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == 0 || _context.Recipes == null)
            {
                return NotFound("Your favorite recipe was not found.");
            }

            Favori2 favori = new Favori2 { RecipeId = id };
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                favori.UserId = userId;
                favori.RecipeId = id;
                _context.Favori2s.Add(favori);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }



    }

}

