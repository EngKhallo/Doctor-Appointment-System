using System.Text;
using System.Text.Json.Serialization;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppointmentsDbContext>(config =>
                config.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;

                    var KeyInput = "random_text_with_atleast_32_chars";
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KeyInput));

                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "MyAPI",
                        ValidateAudience = true,
                        ValidAudience = "MyFrontendApp",
                        ValidateLifetime = true, // Expiration
                        IssuerSigningKey = key
                    };
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
