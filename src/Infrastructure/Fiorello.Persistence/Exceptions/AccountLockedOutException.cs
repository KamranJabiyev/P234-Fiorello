namespace Fiorello.Persistence.Exceptions;

public class AccountLockedOutException:Exception
{
    public AccountLockedOutException(string message) : base(message)
    {
    }
}
