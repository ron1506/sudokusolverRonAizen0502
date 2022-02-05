using System;
using System.Runtime.Serialization;

namespace sudokusolverRonAizen
{
    [Serializable]
    public class InvalidValuesExceptions : Exception
    {
        public InvalidValuesExceptions()
        {
        }

        public InvalidValuesExceptions(string message) : base(message)
        {
        }

        public InvalidValuesExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidValuesExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}