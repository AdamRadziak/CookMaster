using CookMaster.Persistance.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.DTOs
{
    public class AddUpdateUserMenuDTO
    {
        public string Name { get; set; } = null!;
        public int IdUser { get; set; } = 0!;
        public string Category{ get; set; } = null!;
        public ICollection<Recipe> Recipes { get; set; } = null!;




    }
}
