using CQRS.Web.API.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using CQRS.Web.API.Application.Handlers;
using CQRS.Web.API.Infrastructure.DataAccess.Repositories;
using CQRS.Web.API.Infrastructure.Services.Contracts;
using CQRS.Web.API.Infrastructure.Services;
using CQRS.Web.API.Utilidad;
using CQRS.Web.API.Application.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using CQRS.Web.API.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configuracion para que no requiera zona horaria en las fechas de la base de datos de PostgresSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Agregamos servicios definidos en la configuración de dependencias
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCors", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is ValidationException validationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            //configuro la respuesta para validaciones con Fluent Validation en mi clase Response
            var rsp = new Response<object>
            {
                status = false,
                msg = "Errores de validación",
                errors = validationException.Errors.Select(e => new ValidationError
                {
                    PropertyName = e.PropertyName,
                    ErrorMessage = e.ErrorMessage
                }).ToList()
            };

            await context.Response.WriteAsJsonAsync(rsp);
        }
        else if (exception is ArgumentNullException argNullException) //Para execiones cuando hay valores null
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var rsp = new Response<object>
            {
                status = false,
                msg = $"Argumento nulo: {argNullException.ParamName} - {argNullException.Message}"
            };

            await context.Response.WriteAsJsonAsync(rsp);
        }
        else if (exception is TaskCanceledException taskCanceledException) //para cuando da una excepcion y se cancela una task
        {
            context.Response.StatusCode = StatusCodes.Status408RequestTimeout;
            context.Response.ContentType = "application/json";

            var rsp = new Response<object>
            {
                status = false,
                msg = taskCanceledException.Message
            };

            await context.Response.WriteAsJsonAsync(rsp);
        }
        else //Para otra tipos de excepciones
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var rsp = new Response<object>
            {
                status = false,
                msg = "Ha ocurrido un error interno en el servidor.",
                errors = new List<ValidationError> { new ValidationError { PropertyName = "Error", ErrorMessage = exception.Message } }
            };
    
            await context.Response.WriteAsJsonAsync(rsp);
        }

    });
});


app.UseCors("AllowCors");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
