global using Microsoft.EntityFrameworkCore;
global using SuperHeroApi.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RoundtheCode.BasicAuthentication.Shared.Authentication.Basic;
using SuperHeroApi.Repository;
using Microsoft.Extensions.Configuration;
using System.Text;
using SuperHeroApi.ApiKeyAttributes;
using SuperHeroApi.Middleware;
using SuperHeroApi.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;


// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});



builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



// For Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ISuperHeroService, SuperHeroService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(BasicAuthenticationDefaults.AuthenticationSchemes,
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = BasicAuthenticationDefaults.AuthenticationSchemes,
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Basic Authorization header"
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {

                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = BasicAuthenticationDefaults.AuthenticationSchemes
                }
            },
            new string[] {"Basic "}
        }

    });
});

builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
    BasicAuthenticationDefaults.AuthenticationSchemes, null);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ClockSkew = TimeSpan.Zero,

         ValidAudience = builder.Configuration["JWT:ValidAudience"],
         ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
     };
 });


builder.Services.AddSingleton<ApiKeyAuthorizationFilter>();

builder.Services.AddSingleton<IApiKeyValidator, ApiKeyValidator>();

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

app.MapControllers();

app.Run();
