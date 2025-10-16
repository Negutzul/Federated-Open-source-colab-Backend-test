var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Add your own services later here (KeyService, Controllers, etc.)
FGP.Server.Services.KeyGenerator.EnsureKeys();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Add your routes and controllers below
app.MapControllers(); // once you add controllers

app.Run();
