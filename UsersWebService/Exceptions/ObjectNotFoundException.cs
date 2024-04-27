namespace UsersWebService.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(long id) : base($"Object with id {id} not found") { }
        public ObjectNotFoundException(long id, string type) : base($"{type} with id {id} not found") { }
    }
}
