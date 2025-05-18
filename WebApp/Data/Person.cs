namespace WebApp.Data
{
    public class Person
    {
        public int Id { get; init; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; } = int.MinValue;
    }
}
