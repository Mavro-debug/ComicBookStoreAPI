using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Domain.Exceptions
{
    public class InvalidExternalAuthenticationException : Exception
    {
        public InvalidExternalAuthenticationException(string message) : base(message)
        {

        }

        public InvalidExternalAuthenticationException() : base()
        {

        }
    }
}
