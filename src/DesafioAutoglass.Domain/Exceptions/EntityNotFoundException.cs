using System.Collections.Generic;

namespace DesafioAutoglass.Domain.Exceptions
{
    public class EntityNotFoundException : KeyNotFoundException
    {
        public EntityNotFoundException(string type, int id) : base ($"Entity not found. {type.ToUpper()} = {id}")
        {

        }
    }
}
