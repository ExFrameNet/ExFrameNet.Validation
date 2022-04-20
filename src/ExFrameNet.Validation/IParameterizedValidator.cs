namespace ExFrameNet.Validation
{
    public interface IParameterizedValidator<T> : IValidator<T>
    {
        bool Validate(T? value, object parameter);
    }

    public interface IParameterizedValidator<T, TParameter> : IParameterizedValidator<T>
    {
        bool Validate(T value, TParameter parameter);
    }


}
