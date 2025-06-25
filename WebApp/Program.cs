using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc; // for [FromBody]
using System.Collections.Concurrent;
using WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Agency>();

var app = builder.Build();

app.UseStaticFiles(); //  https://localhost:7201/BG3.jpg 
app.UseWelcomePage("/"); 

app.MapGet("/actors", Results<Ok<ConcurrentDictionary<string,Person>>,NoContent> (Agency ag) => ag.Actors.Count > 0 ? TypedResults.Ok(ag.Actors) : TypedResults.NoContent());

app.MapGet("/actors/{id}", Results<Ok<Person>,NotFound> (Agency ag, string id) => ag.Actors.TryGetValue(id, out var actor) ? TypedResults.Ok(actor) : TypedResults.NotFound());

app.MapPost("/actors", Results<Created<Person>, BadRequest> (Agency ag, [FromBody] Person actor) => ag.Actors.TryAdd(actor.LastName, actor) ? TypedResults.Created($"/actors/{actor.LastName}", actor) : TypedResults.BadRequest());

app.MapPut("/actors", Results<NoContent, BadRequest> (Agency ag, [FromBody] Person actor) => 
{
    if (ag.Actors.TryGetValue(actor.LastName, out _))
    {
        ag.Actors[actor.LastName] = actor;
        return TypedResults.NoContent();
    }
    else
    {
        return TypedResults.BadRequest();
    }
}
);

app.MapDelete("/actors/{id}", Results<NoContent, NotFound> (Agency ag, string id) => ag.Actors.TryRemove(id, out _ ) ? TypedResults.NoContent() : TypedResults.NotFound());


app.Run();

