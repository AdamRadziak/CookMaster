using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.DTOs
{
    public class AddUpdateStepDTO
    {
        public int StepNum { get; set; } = 1;
        public int IdRecipe { get; set; } = 0;
        public string Description { get; set; } = null;

        public bool isUpdated { get; set; } = true;

    }
}
