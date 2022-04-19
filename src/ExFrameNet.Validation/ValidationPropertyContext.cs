using ExFrameNet.Utils.Property;

namespace ExFrameNet.Validation
{
    public class ValidationPropertyContext<T,TProperty> : PropertyContext<T,TProperty>
        where T : class
    {

        public ValidationResult ValidationResult { get; set; }

        internal ValidationPropertyContext(PropertyContext<T, TProperty> ctx, ValidationResult result)
            : base(ctx)
        {
            ValidationResult = result;
        }

    }
}
