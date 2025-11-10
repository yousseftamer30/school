using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.Entities;
using DrivingSchoolApi.Services;
using DrivingSchoolApi.Services.Auth;
using DrivingSchoolApi.Services.CurrentUser;
using DrivingSchoolApi.Shared.EncryptText;
using DrivingSchoolApi.Shared.ValidationHandler;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;






var builder = WebApplication.CreateBuilder(args);



SimpleCrypto.Initialize(builder.Configuration);

// Read JWT settings from config
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);


// ✅ Make JSON property name matching case-insensitive globally
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});


builder.Services.AddDbContext<DBContextHRsystem>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Configure password requirements
    options.Password.RequiredLength = 3; // Minimum password length
    options.Password.RequireDigit = false; // Require at least one digit (0-9)
    options.Password.RequireLowercase = false; // Require at least one lowercase letter
    options.Password.RequireUppercase = false; // Require at least one uppercase letter
    options.Password.RequireNonAlphanumeric = false; // Require at least one special character (e.g., !@#$%)
    options.Password.RequiredUniqueChars = 2; // Require at least 4 unique characters
})
    .AddEntityFrameworkStores<DBContextHRsystem>()
    .AddDefaultTokenProviders();





// C# Code - Program.cs or Startup.cs
var alloworg = builder.Configuration.GetSection("Cors:AllowedOrigins");
var allowedOrigins = alloworg.Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm",
        policy =>
        {
            policy.WithOrigins(allowedOrigins ?? Array.Empty<string>())
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

 


// Add services to the container
builder.Services.AddEndpointsApiExplorer(); // Needed for minimal APIs
builder.Services.AddSwaggerGen(options =>
{
    // Swagger support for JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Register JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});

builder.Services.AddAuthorization();


builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(5); // how often to re-check
});

// Replace default with our version
builder.Services.AddScoped<ISecurityStampValidator, PermissionVersionValidator<ApplicationUser>>();


builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineValidationHandler<,>));


builder.Services.AddAutoMapper(typeof(Program));

 

// Register your services
builder.Services.AddScoped<JwtService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();


// builder.Services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
//builder.Services.AddScoped<IAuthorizationHandler, PermissionHandlerService>();

 
builder.Services.AddControllers();


var app = builder.Build();


// Register global exception handler early in the pipeline
app.UseMiddleware<GlobalExceptionMiddleware>();


// Enable Swagger UI in development
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "DrivingSchoolApi v1");

        // options.RoutePrefix = ""; // 👈 makes Swagger the root page
    });

//}

//app.UseSwagger();

//if (app.Environment.IsDevelopment())
//{
//    // 👇 In development: Swagger is the default (root) page
//    app.UseSwaggerUI(options =>
//    {
//        options.SwaggerEndpoint("/swagger/v1/swagger.json", "DrivingSchoolApi v1");
//        options.RoutePrefix = string.Empty; // root access: https://localhost:5001/
//    });
//}
//else
//{
//    // 👇 In production: Swagger only at /swagger
//    app.UseSwaggerUI(options =>
//    {
//        options.SwaggerEndpoint("/swagger/v1/swagger.json", "DrivingSchoolApi v1");
//        // No RoutePrefix override here
//    });
//}




app.UseHttpsRedirection();


// 🔥 apply CORS before authentication/authorization
app.UseCors("AllowBlazorWasm");


app.UseAuthentication(); // Must come before Authorization
app.UseAuthorization();


//builder.Services.AddControllers();

 

app.MapControllers();

 app.Run();




 
