using System;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;

using Models;
using Service;
using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Tilføj DbContext factory som service.
builder.Services.AddDbContext<dbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Tilføj DataService så den kan bruges i endpoints
builder.Services.AddScoped<DataService>();

// Dette kode kan bruges til at fjerne "cykler" i JSON objekterne.
/*
builder.Services.Configure<JsonOptions>(options =>
{
    // Her kan man fjerne fejl der opstår, når man returnerer JSON med objekter,
    // der refererer til hinanden i en cykel.
    // (altså dobbelrettede associeringer)
    options.SerializerOptions.ReferenceHandler = 
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
*/

var app = builder.Build();

// Seed data hvis nødvendigt.
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData(); // Fylder data på, hvis databasen er tom. Ellers ikke.
}



//Endpoints


//Henter alle kunder
app.MapGet("/api/kunder/", (DataService service) =>
{
    return service.GetKunder();
});

//Henter en kunde ud fra id
app.MapGet("/api/kunder/{id}", (DataService service, int id) => {
    return service .GetKundeById(id);
});

//Poster en ny kunde
app.MapPost("/api/post/", (DataService service, Kunde kunde) =>
{
    service.CreateKunde(kunde);

});


app.MapDelete("/api/delete/{id}", (DataService service, int id) =>
{
    service.DeleteKunde(id);
});

app.Run();


//JSON objekt til at teste create kunde endpointet med
/*
{
    "Name": "Morten",
    "Email": "morten@mortensen.dk",
    "Type": "Erhverv"
}
*/