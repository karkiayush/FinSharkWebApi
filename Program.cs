/* This is a builder that is going to control things like dependency injection, it's going to provide you with services & various things that you can add in your program. Almost like a module.*/

using api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adding the ApplicationDBContext.cs in main driver code
builder.Services.AddDbContext<ApplicationDBContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);

var app = builder.Build();

/* The code part below this & above of the Run method is something that is going to be controlling our application pipeline. 

The app variable is going to control the actual HTTP request pipeline, & this is where your middleware is going to be & also can contain various settings that you wanna configure. */

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
// This run method runs the whole backend application
app.Run();
