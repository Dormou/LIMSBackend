using Microsoft.OpenApi.Models;
using testing_managment.ApplicationServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceExtensions();
builder.Services.AddDbServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCors", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v 0.1",
        Title = "Tests BackEnd API",
        Description = "Серверная часть тестов",
    });
});


var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

