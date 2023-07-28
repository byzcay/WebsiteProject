using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipe.Comment
{
    public class IndexModel : PageModel
    {
        private readonly RecipeApp.Models.RecipeAppContext _context;
        public IList<Models.Comment> Comment { get; set; } = default!;
        public IList<Models.UserDetail> userDetails { get; set; } = default!;
        public Models.Recipe Recipe { get; set; } = default!;

        public byte[] Picture { get; set; }

        public IndexModel(RecipeApp.Models.RecipeAppContext context)
        {
            _context = context;
        }

        //public async Task OnGetAsync(int id)
        //{
        //    Comment = await _context.Comments.Where(i => i.RecipeId == id).ToListAsync();
        //    userDetails = await _context.UserDetails.ToListAsync();

        //    if (_context.Comments != null )
        //    {
        //        Recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
        //        ViewData["id"] = id.ToString();

              
        //        ////Assign Photo 
        //        //foreach (var item in Comment)
        //        //{
        //        //    var userDetail = userDetails.FirstOrDefault(u => u.UserId == item.UserId);

        //        //    if (userDetail != null)
        //        //    {
        //        //        Picture = userDetail.Photo;

        //        //        if (Picture == null)
        //        //        {
        //        //            // Read Image File into Image object.
        //        //            string path = "./wwwroot/Images/anonim.jpg";
        //        //            var memoryStream = new MemoryStream();
        //        //            using (var stream = System.IO.File.OpenRead(path))
        //        //            {
        //        //                await new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name)).CopyToAsync(memoryStream);
        //        //                Picture = memoryStream.ToArray();
        //        //            }
        //        //        }
        //        //        else
        //        //        {
        //        //            Picture = userDetail.Photo;
        //        //        }

        //        //    }

        //        //}
        //    }
        //}

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
            Comment = await _context.Comments.Where(i => i.RecipeId == id).ToListAsync();
            return Page();
        }
    }
}

