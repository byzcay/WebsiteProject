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
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipe.Comment
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly RecipeApp.Models.RecipeAppContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public Models.Comment Comment { get; set; } = default!;

        [BindProperty]
        public Models.Recipe? Recipe { get; private set; }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        [BindProperty]
        public string Content { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public string Userid { get; set; }

        public CreateModel(RecipeApp.Models.RecipeAppContext context, 
                               UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsyn(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            Comment = new Models.Comment { UserId = user.Id };
            if (Recipe == null || Recipe.UserId != user.Id)
            {
                return NotFound();
            }
            Recipe = await _context.Recipes.FindAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            Comment = new Models.Comment { RecipeId = id };
            Comment.Content = Content;
            if (!ModelState.IsValid || _context.Comments == null || Comment == null)
            {
                return Page();
            }

            var user = await this._userManager.GetUserAsync(User);
            Comment.UserId = user.Id;

            _context.Comments.Add(Comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Recipe/Index");
        }
    }
}
    

