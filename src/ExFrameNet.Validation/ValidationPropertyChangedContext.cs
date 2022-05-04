using ExFrameNet.Utils.Property;
using System.ComponentModel;

namespace ExFrameNet.Validation;

public class ValidationPropertyChangedContext<T, TPoperty> : PropertyChangedContext<T, TPoperty>
    where T : class, INotifyPropertyChanged
{
    internal List<Action<ValidationResult>> AfterValidationActions;
    internal ValidationPropertyChangedContext(PropertyChangedContext<T, TPoperty> ctx)
        : base(ctx)
    {
        AfterValidationActions = new List<Action<ValidationResult>>();
    }

}
