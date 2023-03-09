namespace BusinessLogicLayer.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success)
        {
            Success = success;
        }
        public Result(bool success, params string[] message) : this(success)
        {
            Message = message;
        }
        public bool Success { get; }

        public IEnumerable<string> Message { get; }
    }
}
