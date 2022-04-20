using ExFrameNet.Utils.Property;

namespace ExFrameNet.Validation
{
    public class ValidationPropertyContext<T,TProperty> : PropertyContext<T,TProperty>
        where T : class
    {

        private readonly ValidationContext<T,TProperty> _context;

        internal ValidationPropertyContext(PropertyContext<T, TProperty> ctx, ValidationContext<T,TProperty> validationContext)
            : base(ctx)
        {
            _context = validationContext;
        }


        public ValidationResult Validate()
        {
            return _context.Validate();
        }

    }
}
