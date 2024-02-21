namespace Gems.AplicationServices.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException() : base() { }
    public EntityNotFoundException(Guid guid) : base($"Entity with id: {guid} is not found") { }


}