namespace BusinessLogicLayer.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult() : base(false) { }
        public ErrorResult(params string[] message) : base(false, message) { }
    }
}
