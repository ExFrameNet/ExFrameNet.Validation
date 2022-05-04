using System.ComponentModel;

namespace ExFrameNet.Validation;

public static class ValidationPropertyChangedContextExtensions
{
    public static ValidationPropertyChangedContext<T, TProperty> AfterValidation<T, TProperty>
        (this ValidationPropertyChangedContext<T, TProperty> ctx, Action<ValidationResult> callback)
        where T : class, INotifyPropertyChanged
    {
        ctx.AfterValidationActions.Add(callback);
        return ctx;
    }
}
