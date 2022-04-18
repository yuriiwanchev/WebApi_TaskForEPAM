using System;

namespace RestApi.Helpers
{
    public class BaseException : Exception
    {
        public BaseException()
        {
        }
        
        public BaseException(string message)
            : base(message)
        {
        }

        public BaseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class InvalidUserInputException : Exception
    {
        public InvalidUserInputException()
        {
        }
        
        public InvalidUserInputException(string message)
            : base(message)
        {
        }

        public InvalidUserInputException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}