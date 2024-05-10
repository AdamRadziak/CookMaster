using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.Utils
{
    public static class PhotoTools
    {
        public static byte[] ConvertFromFile2Byte(string path)
        {

            byte[] photo = File.ReadAllBytes(Path.GetFullPath(path));
            return photo;
        }
    }
}
