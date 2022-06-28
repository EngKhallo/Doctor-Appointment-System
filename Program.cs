using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppointmentsDbContext>(config =>
                config.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.
builder.Services.AddControllers()
	.AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddAuthentication("Bearer") // Token: JWT = Json Web Token
    .AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false;

        var keyInput = "random_text_with_at_least_32_chars";

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyInput));

        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "MyAPI",
            ValidateAudience = true,
            ValidAudience = "MyFrontendApp",
            ValidateLifetime = true,
            IssuerSigningKey = key
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// what is beyond this is called : Middlewares

// Authentication vs Authorization the
// Authentication : Who are you? -- Login 
// Authorization : Are you able to do this action? -- Role

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
