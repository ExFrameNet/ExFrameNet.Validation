namespace ExFrameNet.Validation;

public class ValidationOptions
{
    public bool BreakAfterFirstFail { get; init; }


    public static ValidationOptions Default =>
        new()
        {
            BreakAfterFirstFail = false
        };
}
