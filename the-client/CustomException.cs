using System;
using System.Net;

namespace ClientLib
{
    public class CustomException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }

        public CustomException(HttpStatusCode httpStatusCode, string responseMessage)
            : base(responseMessage)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
