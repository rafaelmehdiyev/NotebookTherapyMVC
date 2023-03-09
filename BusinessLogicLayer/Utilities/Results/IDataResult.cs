namespace BusinessLogicLayer.Utilities.Results;

public interface IDataResult<T> : IResult
{
    public T Data { get; }
}