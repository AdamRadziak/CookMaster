using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.DTOs
{
    public class GetSingleStepDTO
    {
        public int Id { get; set; }
        public int StepNum { get; set; }
        public string Description { get; set; } = null!;
    }
}
