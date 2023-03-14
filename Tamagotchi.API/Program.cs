using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Tamagotchi.API.Security;
using Tamagotchi.API.Services;
using Tamagotchi.API.Services.Interfaces;
using Tamagotchi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFK";
    options.SerializerSettings.Converters = new List<JsonConverter>
    {
        new StringEnumConverter()
    };
});

builder.Services.AddSingleton<JwtConfig>(o =>
{
    var config = new JwtConfig();
    builder.Configuration.GetSection("Authentication:JwtConfig").Bind(config);
    return config;
});

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    var config = new JwtConfig();
    builder.Configuration.GetSection("Authentication:JwtConfig").Bind(config);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config.Issuer,
        ValidAudience = config.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret))
    };
});

builder.Services.AddApiVersioning(config =>
{
    // Specify the default API Version
    config.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TamagotchiDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IFoodsService, FoodService>();
builder.Services.AddScoped<IPetsService, PetService>();
builder.Services.AddScoped<ISpeciesService, SpeciesService>();
builder.Services.AddScoped<IUsersService, UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    //First we define the security scheme
    c.AddSecurityDefinition("Bearer", //Name the security scheme
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
            Scheme = "bearer", //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
            In = ParameterLocation.Header,
            Name = "Authorization"
            
        });
    
    //Next we assign the security scheme to the endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    
});

var app = builder.Build();

// Let's explicitly migrate the database
var databaseContext = app.Services.CreateScope().ServiceProvider
    .GetRequiredService<TamagotchiDbContext>();

databaseContext.Database.Migrate();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "API"); });
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();