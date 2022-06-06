using System;
using System.Collections.Generic;

namespace DesafioAutoglass.Domain.Exceptions
{
    public class ModelValidationException : Exception
    {
        public ICollection<string> ValidationMessages { get; set; }

        public ModelValidationException(string message)
        {
            ValidationMessages = new List<string> { message };
        }

        public ModelValidationException(List<string> errors) 
        {
            ValidationMessages = errors;
        }
    }
}
