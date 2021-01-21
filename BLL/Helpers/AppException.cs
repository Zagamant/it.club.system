using System.Globalization;

namespace System.BLL.Helpers
{
    /// <summary>
    /// Custom exception class for throwing application specific exceptions (e.g. for validation) that can be caught and handled within the application
    /// </summary>
    public class AppException : Exception
    {
        public AppException() : base("Internal app exception") { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
