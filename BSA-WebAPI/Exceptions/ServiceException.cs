namespace BSA_WebAPI.Exceptions;

public class ServiceException: Exception
{
    public ServiceException(string message)
        :base(message)
    {
        
    }
}