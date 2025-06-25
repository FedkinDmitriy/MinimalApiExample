using System.Collections.Concurrent;

namespace WebApp.Data
{
    public class Agency
    {
        public ConcurrentDictionary<string, Person> Actors { get; } = new();

        public Agency()
        {
            Actors.TryAdd("Hardy", new Person() { FirstName = "Tom", LastName = "Hardy", Age = 40});
            Actors.TryAdd("Cruz", new Person() { FirstName = "Tom", LastName = "Cruz", Age = 60});
            Actors.TryAdd("Pitt", new Person() { FirstName = "Brad", LastName = "Pitt", Age = 55});
            Actors.TryAdd("Sandler", new Person() { FirstName = "Adam", LastName = "Sandler", Age = 50});
            Actors.TryAdd("DiCaprio", new Person() { FirstName = "Leonardo", LastName = "Di Caprio", Age = 40});
        }
    }
}
