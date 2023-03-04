using PaymentGateway.API.Data;
using PaymentGateway.API.Services;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.API.Payments.Data;
using PaymentGateway.API.Exceptions;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
HttpConfiguration HttpConfiguration = new HttpConfiguration();

// Add services to the container.
builder.Services.AddScoped<IPaymentsService, PaymentsService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

//this gets refreshed with every build
builder.Services.AddDbContext<PaymentDbContext>(options =>
{
    options.UseInMemoryDatabase("Payments");
});

builder.Services.AddHttpClient("BankSimulator", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:BankSimulator"]);
});

// Configuring authentication via the Identity Server project
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";
        options.Audience = "pg-api";
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false
        };
    });


builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adding custom global level exception logger
HttpConfiguration.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Exception Handlers
app.ConfigureCustomExceptionHandler();

app.MapControllers();

app.Run();
