using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.DTOs
{
    public class GetSingleProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public double Amount { get; set; } = 0!;
        public string Unit { get; set; } = null!;
    }
}
