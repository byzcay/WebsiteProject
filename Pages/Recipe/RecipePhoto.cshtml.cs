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
    public class RecipePhotoModel : PageModel
    {
        private readonly RecipeApp.Models.RecipeAppContext _context;

        [BindProperty]
        public BufferedSingleFileUploadDb? FileUpload { get; set; }

        public byte[] Picture { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RecipeId { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
       
        public RecipePhotoModel(RecipeApp.Models.RecipeAppContext context)
        {
            _context = context;
        }


        //private async Task LoadAsync()
        //{
        //    var recipePhoto = _context.Recipes.FindAsync(Models.Recipe.); 
        //    if (recipePhoto != null) //veritabanından okuma
        //    {
        //        Picture = recipePhoto.Image;
        //    }
        //    else  //pathden okuma 
        //    {
        //        //Read Image File into Image object.
        //        string path = "./wwwroot/Images/anonim.jpg";
        //        var memoryStream = new MemoryStream();
        //        using (var stream = System.IO.File.OpenRead(path))
        //        {
        //            await new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name)).CopyToAsync(memoryStream);
        //            Picture = memoryStream.ToArray();
        //        };
        //    }

        //}


        //public async Task OnGetAsync(int recipeId)
        //{
        //    ViewData["RecipeID"] = recipeId.ToString();
        //    Recipe = await _context.Recipes.FindAsync(recipeId);
        //    RecipeId = recipeId;
        //    //Recipe = await recipe.ToListAsync();

        //}
    }
}
