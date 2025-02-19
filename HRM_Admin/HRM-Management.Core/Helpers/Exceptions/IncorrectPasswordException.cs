namespace HRM_Management.Core.Helpers.Exceptions
{
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException()
            : base("Provided password or username are not correct!")
        {
        }

        public IncorrectPasswordException(string message)
            : base(message)
        {
        }

        public IncorrectPasswordException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
