using System;

namespace CleanArchitecture.Demo.Application
{
    public class ProductExpiredException: Exception
    {
        public ProductExpiredException()
        {
        }

        public ProductExpiredException(string message)
            : base(message)
        {
        }

        public ProductExpiredException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
