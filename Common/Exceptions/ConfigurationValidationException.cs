using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class ConfigurationValidationException : Exception
    {
        public ConfigurationValidationException()
        {
        }
        public ConfigurationValidationException(string message) : base(message)
        {
        }
        public ConfigurationValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
