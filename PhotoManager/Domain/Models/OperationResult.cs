
using FluentValidation.Results;

namespace Domain.Models
{
    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }
    }

    public class OperationResult
    {
        public bool IsValidationError { get; set; }

        public ValidationResult ValidationResult { get; set; }
    }
}
