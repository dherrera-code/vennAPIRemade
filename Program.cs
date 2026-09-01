using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using vennAPIRemade.Context;
using vennAPIRemade.Interface.IRepo;
using vennAPIRemade.Interface.IService;
using vennAPIRemade.Repository;
using vennAPIRemade.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
       In = ParameterLocation.Header,
       Description = "Enter your JWT token. Example: eyblahblahblah...",
       Name = "Authorization",
       Type = SecuritySchemeType.ApiKey,
       Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {{
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
    }});
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomMemberService, RoomMemberService>();
builder.Services.AddScoped<IRoomMemberRepository, RoomMemberRepository>();
builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<IFriendRepository, FriendRepository>();
builder.Services.AddSingleton<BlobService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // This explicitly ignores cycles!
});


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program));
    // cfg.LicenseKey = Environment.GetEnvironmentVariable("AutoMapperKey");
    cfg.LicenseKey = builder.Configuration["AutoMapper"];
});

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

var secretKey = builder.Configuration["JWT:Key"] ?? "superSecretKey@345superSecretKey@345";
var signingCredentials = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = "https://vennbackendapi-akghachgbhgdccfe.westus3-01.azurewebsites.net",
        ValidAudience = "https://vennbackendapi-akghachgbhgdccfe.westus3-01.azurewebsites.net",

        // The key used to sign the token (must match the one used to create it)
        IssuerSigningKey = signingCredentials
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Development", policy =>
    {
       policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "https://venn-iota.vercel.app"); 
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("Development");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();