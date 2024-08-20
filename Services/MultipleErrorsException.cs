namespace USFWebAPI.Services
{
    public class MultipleErrorsException : Exception
    {
        public List<Exception> Exceptions { get; }
        public MultipleErrorsException(List<Exception> exceptions)
        {
            Exceptions = exceptions;
        }
    }
}
