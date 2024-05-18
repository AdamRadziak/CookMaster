using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.DTOs
{
    public class GetSingleRecipeDTO
    {
        public int Id { get; set; }
        public string PrepareTime { get; set; } = null!;
        public int MealCount { get; set; } = 0!;
        public double Rate { get; set; } = 0.0!;
        public double Popularity { get; set; } = 0.0!;
        public string Description { get; set; } = null!;
        public string[] PhotoNames { get; set; } = null!;
        public byte[] PhotoData { get; set; }=null!;

        public string[] Steps { get; set; } = null!;
        public string[] ProductNames { get; set; } = null!;

        public string[] ProductAmounts { get; set; } = null!;
    }
}
