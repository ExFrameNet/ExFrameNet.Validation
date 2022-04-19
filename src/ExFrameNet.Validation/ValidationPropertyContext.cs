using ExFrameNet.Utils.Property;

namespace ExFrameNet.Validation
{
    public class ValidationPropertyContext<T,TProperty> : PropertyContext<T,TProperty>
        where T : class
    {

        internal ValidationPropertyContext(PropertyContext<T, TProperty> ctx)
            : base(ctx)
        {
        }

    }
}
