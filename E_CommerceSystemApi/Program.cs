using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System;
using Microsoft.EntityFrameworkCore;
using E_CommerceSystemApi.DAL.Data;
using E_CommerceSystemApi.BLL.Services.impl;
using E_CommerceSystemApi.BLL.Services.intf;
using E_CommerceSystemApi.DAL.Repository.intf;
using E_CommerceSystemApi.DAL.Repository.impl;
using E_CommerceSystemApi.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using E_CommerceSystemApi.Services;
using Microsoft.OpenApi.Models;





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection for Services and Repositories
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// AutoMapper registered here
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Bind the "Jwt" section from appsettings.json to the JwtSettings class
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("Jwt"));

// Configure the authentication system to use JWT Bearer as the default scheme
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
 // Configure the JWT Bearer middleware with token validation rules
    .AddJwtBearer(options =>
{
  var jwt = builder.Configuration.GetSection("Jwt").Get<JWTSettings>();
  options.SaveToken = true;
options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = jwt.Issuer,
    ValidAudience = jwt.Audience,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SigningKey))
};
});

//Add authorization services 
builder.Services.AddAuthorization();

// Register the JwtTokenService for generating JWT tokens
builder.Services.AddScoped<JwtTokenService>();

// Database Context Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));


// Add Swagger configuration including JWT Bearer authentication support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E_CommerceSystemApi", Version = "v1" });

    //Add JWT Bearer Auth
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.",
    });

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
            new string[] {}
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
// 
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
