namespace ATag.Core
{
    using System;

    public class TagException : Exception
    {
        public TagException(string message)
            : base(message)
        {
        }

        public TagException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}