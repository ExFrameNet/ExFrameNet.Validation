namespace ExFrameNet.Validation
{
    public class ValidationResult
    {

        public string PropertyName { get; }
        public bool IsValid => !Errors.Any();
        public IEnumerable<ValidationError> Errors { get; }
        

        public ValidationResult(string propertyName, IEnumerable<ValidationError> errors)
        {
            PropertyName = propertyName;
            Errors = errors;
        }
    }
}
