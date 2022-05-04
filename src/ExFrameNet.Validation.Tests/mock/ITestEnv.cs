namespace ExFrameNet.Validation.Tests;

public interface ITestEnv : IValidatable
{
    string StringProp { get; }

    object ObjProp { get; }

    int IntProp { get; }
}

