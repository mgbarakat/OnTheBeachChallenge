using System;
using System.Runtime.Serialization;

namespace OnTheBeachChallenge
{
    [Serializable]
    public class SelfReferencingException : Exception
    {
        public SelfReferencingException()
        {
        }

        public SelfReferencingException(string message) : base(message)
        {
        }

        public SelfReferencingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SelfReferencingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class CircularDependencyException : Exception
    {
        public CircularDependencyException()
        {
        }

        public CircularDependencyException(string message) : base(message)
        {
        }

        public CircularDependencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CircularDependencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}