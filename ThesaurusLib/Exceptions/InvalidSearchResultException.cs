using System;
using System.Runtime.Serialization;

namespace Ric.ThesaurusLib.Exceptions
{
    [Serializable]
    public class InvalidSearchResultException : ThesaurusExceptionBase
    {
        public InvalidSearchResultException()
        {
        }

        public InvalidSearchResultException(string message) : base(message)
        {
        }

        public InvalidSearchResultException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidSearchResultException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}