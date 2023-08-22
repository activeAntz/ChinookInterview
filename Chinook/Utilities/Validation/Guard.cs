using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Chinook.Utilities.Validation
{
    public class Guard
    {
        public static void ThrowIfNull(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Value cannot be null.", (Exception)null);
        }

        public static void ThrowIfEmptyString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("String cannot be null or empty.");
        }

        public static void ThrowIfObjectNotFound(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Data object not found");
        }
    }
}
