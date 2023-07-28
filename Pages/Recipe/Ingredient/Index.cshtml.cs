﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipe.Ingredient
{
    public class IndexModel : PageModel
    {
        private readonly RecipeApp.Models.RecipeAppContext _context;

        public IndexModel(RecipeApp.Models.RecipeAppContext context)
        {
            _context = context;
        }

        public IList<Models.Ingredient> Ingredient { get;set; } = default!;
        public Models.Recipe Recipe { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
            ViewData["id"] = id.ToString();

            if (Recipe == null)
            {
                return NotFound();
            }

            Ingredient = await _context.Ingredients.Where(i => i.RecipeId == id).ToListAsync();

            return Page();
        }
    }
}
