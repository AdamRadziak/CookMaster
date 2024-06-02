using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.DTOs
{
    public class AddUpdatePhotoDTO
    {
        public int IdRecipe { get; set; } = 0;
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;

        public bool IsUpdate { get; set; } = true;
    }
}
