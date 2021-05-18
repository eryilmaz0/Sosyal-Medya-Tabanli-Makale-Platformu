using System.Collections.Generic;

namespace Project.Core.ValidationErrorObjects
{
    public class ValidationResult
    {
        public List<ValidationError> ValidationErrors { get; set; }


        public ValidationResult()
        {
            this.ValidationErrors = new List<ValidationError>();
        }
    }
}