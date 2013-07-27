using System;
using System.Runtime.Serialization;

namespace TfsLamp.Infrastructure.Rendering
{
    public class RenderException : Exception
    {
        public RenderException()
        {
        }

        public RenderException(string message) : base(message)
        {
        }

        public RenderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RenderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}