namespace HRM_Management.Core.Helpers.Exceptions
{
    public class IdentifierAlreadyTakenException : Exception
    {
        public IdentifierAlreadyTakenException()
            : base("Provided username is alreaedy taken!")
        {
        }

        public IdentifierAlreadyTakenException(string message)
            : base(message)
        {
        }

        public IdentifierAlreadyTakenException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
