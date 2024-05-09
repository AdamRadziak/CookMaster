using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.DTOs
{
    public class GetSingleUserMenuDTO
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public string Name { get; set; }
        public string Recipe { get; set;} = null!;
        public string RecipePrepareTime { get; set; } = null!;
        public string RecipeMealCount { get; set; } = null!;    
        public string RecipeRate { get; set; } = null!;  
        public string RecipePopularity {  get; set; } = null!;
        public string RecipeDescription { get; set; } = null!;


    }
}
