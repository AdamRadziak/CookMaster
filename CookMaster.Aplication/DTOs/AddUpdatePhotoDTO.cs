using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.DTOs
{
    public class AddUpdatePhotoDTO
    {
        public string FileName { get; set; } = null!;
        public byte[]? Data { get; set; } = null;
        public string FilePath { get; set; } = null!;

        public bool IsUpdate { get; set; } = true;
    }
}
