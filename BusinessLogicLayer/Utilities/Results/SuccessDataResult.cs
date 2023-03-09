namespace BusinessLogicLayer.Utilities.Results;

public class SuccessDataResult<T> : DataResult<T>
{
	public SuccessDataResult() : base(default,true) { }
    public SuccessDataResult(T data) : base(data, true) { }

    public SuccessDataResult(T data, params string[] message) : base(data, true, message) { }
    public SuccessDataResult(params string[] message) : base(default, true, message) { }
}
