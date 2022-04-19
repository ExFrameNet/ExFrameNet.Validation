namespace ExFrameNet.Validation
{
    public abstract class AbstractParameterizedValidator<T, TParameter> : AbstractValidator<T>, IParameterizedValidator<T, TParameter>
    {
       
        public abstract bool Validate(T? value, TParameter parameter);

        public bool Validate(T? value, object parameter)
        {
            
            
            return Validate(value, (TParameter)parameter);
        }

        public  override bool Validate(T? value)
        {
            throw new InvalidOperationException("Validator needs a Parameter");
        }
    }
}
