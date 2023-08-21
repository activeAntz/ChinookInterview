namespace Chinook.Utilities.Validation
{
    public class Guard
    {
        public static void ThrowIfNull(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Value cannot be null.", (Exception)null);
        }

        public static void ThrowIfNull(object obj, string paramName)
        {
            if (obj == null)
                throw new ArgumentNullException(paramName);
        }
        public static void ThrowIfEmptyString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("String cannot be null or empty.");
        }

        public static void ThrowIfEmptyString(string str, string message)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException(message);
        }
    }
}
