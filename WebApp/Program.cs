using WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

List<Person> persons = [new Person { Id = 1, FirstName = "Tom", LastName = "Kukuruz", Age = 50},
    new Person { Id = 2, FirstName = "Vin", LastName = "Benzin", Age = 45 },
    new Person { Id = 3, FirstName = "John", LastName = "Doe", Age = 33}];

var app = builder.Build();


app.MapGet("/", () => "Something text for someone");
app.MapGet("/persons", () => persons);
app.MapGet("/persons/{id}", (int id) =>
{
    var person = persons.FirstOrDefault(p => p.Id == id);
    return person is not null ? Results.Ok(person) : Results.NotFound();
});


app.MapPost("/persons", (Person newPerson) => {

    var existing = persons.FirstOrDefault(p => p.Id == newPerson.Id);

    if (existing is null)
    {
        persons.Add(newPerson);
        return Results.Created($"/persons/{newPerson.Id}", newPerson);
    }
    else return Results.Conflict($"Person with id {newPerson.Id} already exists");
});

app.MapPut("/persons", (Person updatedPerson) =>
{
    var person = persons.FirstOrDefault(p => p.Id == updatedPerson.Id);
    if (person is null)
    {
        return Results.NotFound($"Person with id {updatedPerson.Id} not found");
    }

    person.FirstName = updatedPerson.FirstName;
    person.LastName = updatedPerson.LastName;
    person.Age = updatedPerson.Age;

    return Results.Ok(person);
});

app.MapDelete("/persons/{id}", (int id) =>
{
    var person = persons.FirstOrDefault(p => p.Id == id);
    if(person is null)
    {
        return Results.NotFound();
    }
    else
    {
        persons.Remove(person);
        return Results.Ok();
    }
});


app.Run();

