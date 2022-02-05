using System;
using System.Runtime.Serialization;

namespace sudokusolverRonAizen
{
    [Serializable]
    public class UnsolveableBoardException : Exception
    {
        public UnsolveableBoardException()
        {
        }

        public UnsolveableBoardException(string message) : base(message)
        {
        }

        public UnsolveableBoardException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsolveableBoardException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}