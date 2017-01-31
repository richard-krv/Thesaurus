using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ric.ThesaurusLib.Exceptions
{
    public abstract class ThesaurusExceptionBase : Exception
    {
        public ThesaurusExceptionBase()
        {
        }

        public ThesaurusExceptionBase(string message) : base(message)
        {
        }

        public ThesaurusExceptionBase(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ThesaurusExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
