namespace ExFrameNet.Validation
{
    public class ValidationError
    {
        public string ErrorMessage { get; }
        public uint ErrorCode { get; }
        public object? CustomError { get; }
        public object? AttemptedValue { get; }
        public Severity Severity { get; }

        public ValidationError(string errorMessage, uint errorCode, object? attemptedValue, Severity severity = Severity.Error, object? customError = null)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            CustomError = customError;
            AttemptedValue = attemptedValue;
            Severity = severity;
        }

        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}
