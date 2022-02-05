using System;
using System.Runtime.Serialization;

namespace sudokusolverRonAizen
{
    [Serializable]
    public class InvalidSizeException : Exception
    {
        public InvalidSizeException()
        {
        }

        public InvalidSizeException(string message) : base(message)
        {
        }

        public InvalidSizeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}