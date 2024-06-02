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
        public int? IdUser { get; set; } = 0!;
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public ICollection<GetSingleRecipeDTO> Recipes { get; set; } = null!;


    }
}
