using System;

namespace Domain.Helpers
{
    public class AlreadyExistenceException : Exception
    {
        public AlreadyExistenceException()
        {
        }
        
        public AlreadyExistenceException(string message)
            : base(message)
        {
        }

        public AlreadyExistenceException(string message, Exception inner)
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