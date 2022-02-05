using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace sudokusolverRonAizen
{
    /// <summary>
    /// handles with exceptions with the file choosing from the file dialog.
    /// </summary>
    [Serializable]
    public class FileChoosingException : Exception
    {
        public FileChoosingException()
        {
        }

        public FileChoosingException(string message) : base(message)
        {
        }

        public FileChoosingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileChoosingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
