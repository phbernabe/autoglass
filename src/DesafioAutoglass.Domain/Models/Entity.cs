namespace DesafioAutoglass.Domain.Models
{
    public abstract class Entity<T> 
    {
        public T Id { get; private set; }
    }
}
