using System;
using System.Collections.Generic;

namespace OrderSheet.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures has occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IReadOnlyDictionary<string, string[]> errors)
            : this()
        {
            Errors = errors;
        }

        public IReadOnlyDictionary<string, string[]> Errors { get; }
    }
}
