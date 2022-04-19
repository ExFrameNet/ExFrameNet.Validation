namespace ExFrameNet.Validation
{
    internal class ValidatorAttachments
    {
        

        public string Message { get; set; }
        public uint ErrorCode { get; set; }
        public object? CustomError { get; set; }
        public Severity Severity { get; set; } = Severity.Error;

        public ValidatorAttachments(string message, uint errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
        }
    }
}
