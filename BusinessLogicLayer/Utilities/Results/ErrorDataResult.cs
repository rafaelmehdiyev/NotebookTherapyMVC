namespace BusinessLogicLayer.Utilities.Results;

public class ErrorDataResult<T> : DataResult<T>
{
    public ErrorDataResult() : base(default, false) { }
    public ErrorDataResult(T data) : base(data, false) { }

    public ErrorDataResult(T data, params string[] message) : base(data, false, message) { }
    public ErrorDataResult(params string[] message) : base(default, false, message) { }
}