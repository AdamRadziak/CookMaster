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
        public int IdRecipe { get; set; } = 0;
        public string Category {  get; set; } = null!;
        public string Amount { get; set; } = null!;
        public bool IsUpdated { get; set; } = true;

    }
}
