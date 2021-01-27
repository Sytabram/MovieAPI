using System;

namespace MovieAPI.Exceptions
{
    public class DataNotFoundException: Exception
    {
        public DataNotFoundException(string message) : base(message)
        {
        }
    }
}