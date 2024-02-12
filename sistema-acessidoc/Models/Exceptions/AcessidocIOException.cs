using System.Runtime.Serialization;

namespace sistema_acessidoc.Models.Exceptions
{
    public class AcessidocIOException : IOException
    {
        public AcessidocIOException() { }
        public AcessidocIOException(string? message) : base(message) { }
        public AcessidocIOException(string? message, Exception? innerException) : base(message, innerException) { }
        protected AcessidocIOException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}