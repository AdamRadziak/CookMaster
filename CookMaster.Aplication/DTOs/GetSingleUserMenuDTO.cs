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
        public string Name { get; set; } = null!;
        public string RecipeCategory { get; set; } = null!;
        public ICollection<GetSingleRecipeDTO> Recipes { get; set; } = null!;


    }
}
