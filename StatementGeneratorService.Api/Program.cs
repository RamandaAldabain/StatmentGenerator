using FluentValidation.AspNetCore;
using StatementGeneratorService.Application.Validators;
using Microsoft.EntityFrameworkCore;
using StatementGeneratorService.Application.Features.Statements.Commands.GenerateStatement;
using StatementGeneratorService.Application.Interfaces;
using StatementGeneratorService.Application.IRepositories;
using StatementGeneratorService.Application.Repositories;
using StatementGeneratorService.Application.Services;
using StatementGeneratorService.Domain.Entites;
using StatementGeneratorService.Infrastructure.Database;
using FluentValidation;
using StatementGeneratorService.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Configure Swagger
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        ));
});

// Services Dependency Injection 
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IStatementService, StatementService>();
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TransactionDtoValidator>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IStatementRepository, StatementRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GenerateStatementCommandHandler)));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // swagger exampels
    app.UseStaticFiles();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = string.Empty;
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StatementGeneratorService API V1");
        c.InjectJavascript("/swagger-custom.js");
    });
}

// middleware for global exception handling and request logging
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();