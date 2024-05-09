using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.DTOs
{
    public class AddUpdateProductDTO
    {
        public string Name { get; set; } = null!;
        public string Category {  get; set; } = null!;
        public string Amount { get; set; } = null!;
        public int RecipeId { get; set; } = 0;
        public bool IsUpdated { get; set; } = true;

    }
}
