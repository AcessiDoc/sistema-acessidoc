using System.Runtime.Serialization;

namespace sistema_acessidoc.Models.Exceptions
{
    [Serializable]
    public class AcessidocException : Exception
    {
        public AcessidocException() { }
        public AcessidocException(string? message) : base(message) { }
        public AcessidocException(string? message, Exception? innerException) : base(message, innerException) { }
        protected AcessidocException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}