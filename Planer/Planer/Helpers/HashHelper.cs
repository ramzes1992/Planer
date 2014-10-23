using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Planer.Helpers
{
    public static class HashHelper
    {
        public static byte[] GetHash(string input)
        {
            byte[] hash;
            using (var md5 = MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            }

            return hash;
        }

        public static Guid GetHashAsGuid(string input)
        {
            byte[] hash;
            using (var md5 = MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            }

            if (hash == null)
            {
                throw new CryptographicException("GetHashAsGuid FAIL!");
            }
            
            return new Guid(hash);
        }
    }
}
