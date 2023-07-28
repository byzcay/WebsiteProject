using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipe
{
    [Authorize]
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
        public Models.Recipe Recipe { get; set; } = default!;

        //[BindProperty]
        //public BufferedSingleFileUploadDb? FileUpload { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            this.Recipe = new Models.Recipe { UserId = user.Id };

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid || _context.Recipes == null || Recipe == null)
            {
                return Page();
            }

            var user = await this._userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            Recipe.UserId = user.Id;
            _context.Recipes.Add(Recipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Recipe/Index");
        }
    }
}
