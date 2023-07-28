using System;
using System.Collections.Generic;

namespace RecipeApp.Models;

public partial class Favori2
{
    public int Id { get; set; }

    public int? RecipeId { get; set; }

    public string? UserId { get; set; }

    public int? FavValue { get; set; }
}
