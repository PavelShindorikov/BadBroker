namespace Application.Common.Exceptions;

public class RateServiceException : Exception
{
    public RateServiceException() : base() { }
    public RateServiceException(string message) : base(message) { }
    public RateServiceException(string message, Exception innerException) : base(message, innerException) { }
}