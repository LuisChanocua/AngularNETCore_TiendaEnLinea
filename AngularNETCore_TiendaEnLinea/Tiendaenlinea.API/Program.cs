using Tiendaenlinea.IOC;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Extension de la DB conection
builder.Services.InyectarDependencias(builder.Configuration);

builder.Services.AddCors(
    o =>
    {
        o.AddPolicy("CorsPolytic", p =>
        {
            p.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
    }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolytic");
app.UseAuthorization();
app.MapControllers();
app.Run();
