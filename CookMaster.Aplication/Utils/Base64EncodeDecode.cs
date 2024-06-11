using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.Utils
{
    public static class Base64EncodeDecode
    {
        public static string Base64Encode(string plainText)
        {
            var cleanedText = plainText.Replace(" ", "").Replace("\n", " ");
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(cleanedText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            // decode string from base64
            string decodedText = Encoding.UTF8.GetString(base64EncodedBytes);
            // Remove white spaces and newline characters
            string cleanedText = decodedText.Replace(" ", "").Replace("\n", "");
            return cleanedText;
        }
    }
}
