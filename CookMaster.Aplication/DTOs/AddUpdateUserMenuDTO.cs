using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.DTOs
{
    public class AddUpdateUserMenuDTO
    {
        public string Name { get; set; }
        public string UserEmail { get; set; } = null!;
        public string RecipeDescription { get; set; } = null!;
        public ICollection<GetSingleRecipeDTO> Recipes { get; set; } = null!;




    }
}
