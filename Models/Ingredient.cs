using System;
using System.Collections.Generic;

namespace RecipeApp.Models;

public partial class Ingredient
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Quantity { get; set; }

    public string? Unit { get; set; }

    public int? RecipeId { get; set; }

    public int? Calories { get; set; }
}
