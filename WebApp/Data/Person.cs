namespace WebApp.Data
{
    public class Person
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; } = int.MinValue;
    }
}
