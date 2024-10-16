using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;  // Adicionado para Swagger
using YourNamespace.Data; // Certifique-se de que este namespace est� correto

var builder = WebApplication.CreateBuilder(args);

// Adicionando o servi�o de controle e Swagger
builder.Services.AddControllers();

// Configura��o do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Folix API",
        Version = "v1",
        Description = "API para o sistema Web Folix",
        Contact = new OpenApiContact
        {
            Name = "Seu Nome",
            Email = "seuemail@example.com",
            Url = new Uri("https://seusite.com")
        }
    });
});

// Configura��o do DbContext com a string de conex�o
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Habilitar Swagger apenas no ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Folix API v1");
        c.RoutePrefix = string.Empty;  // Isso faz com que o Swagger seja acess�vel diretamente na raiz
    });
}

app.UseCors("AllowAllOrigins");
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
