using System;

namespace ShortBook.SDK.CSharp
{
    public class ClientException : ApplicationException
    {
        public ClientException(string message) : base(message){}
    }
}