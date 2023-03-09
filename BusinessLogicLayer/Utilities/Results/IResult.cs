namespace BusinessLogicLayer.Utilities.Results
{
    public interface IResult
    {
        public bool Success { get; }
        public IEnumerable<string> Message { get; }
    }
}
