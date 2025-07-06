using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Auth1.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var tokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuer = true,
    ValidateLifetime = true,
    ValidateAudience = true,
    ValidIssuer =  builder.Configuration["Issuer"],
    ValidAudience = builder.Configuration["Audience"], 
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Secret"] ?? throw new Exception("Нету секрета в конфиге")))
};

builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option => //делаем так, шоб сваггер стать авторизация-шанхай-машина
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = tokenValidationParameters;
    });

builder.Services.AddDbContextFactory<AuthDbContext>(optionsBuilder =>
{
    optionsBuilder
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var tokenHandler = new JwtSecurityTokenHandler();

app.Map("/auth", (HttpContext context) =>
{

    var token = context.Request.Headers["Authorization"].ToString();
    tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken); //херню делаешь. Тебе нужно просто проверить, выписан ли этот токен тобой.
    
    //тут мне надо сделать валидацию токена
});
    
app.MapControllers();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.Run();
