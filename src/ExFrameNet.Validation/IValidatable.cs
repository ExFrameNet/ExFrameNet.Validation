namespace ExFrameNet.Validation
{
    public interface IValidatable
    {
        HashSet<string> Validproperties { get; }
        List<ValidationContext> ValidationContexts { get; }

        void OnPropertyValidated(ValidationResult result);
    }
}
