using CookMaster.Persistance.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.DTOs
{
    public class GenerateUserMenuDTO
    {
        public string Name { get; set; } = null!;
        public int IdUser { get; set; } = 0!;
        public string Category { get; set; } = null!;
        public int DayCount { get; set; } = 1!;
        public int MealCount { get; set; } = 2!;
        public double Rate { get; set; } = 0!;
        public double Popularity {get; set; } = 0!;
        public int PrepareTime {  get; set; } = 120!;


    }
}
