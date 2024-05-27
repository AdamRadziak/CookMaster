using CookMaster.Persistance.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.DTOs
{
    public class AddUpdateRecipeDTO
    {
        public string Name { get; set; } = null!;
        public string PrepareTime { get; set; } = null!;
        public int MealCount { get; set; } = 0!;
        public double Rate { get; set; } = 0.0!;
        public double Popularity { get; set; } = 0.0!;
        public string Description { get; set; } = null!;

        public bool IsRecipeUpdate { get; set; } = true;


    }
}
