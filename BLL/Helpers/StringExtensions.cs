using System.Linq;

namespace System.BLL.Helpers
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => char.ToUpper(input.First()) + input[1..]
            };
    }
}