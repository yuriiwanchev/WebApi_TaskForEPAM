using System;

namespace RestApi.Helpers
{
    public class LectureNameNotFoundException : Exception
    {
        public LectureNameNotFoundException()
        {
        }

        public LectureNameNotFoundException(string message)
            : base(message)
        {
        }

        public LectureNameNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class StudentNameNotFoundException : Exception
    {
        public StudentNameNotFoundException()
        {
        }

        public StudentNameNotFoundException(string message)
            : base(message)
        {
        }

        public StudentNameNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}