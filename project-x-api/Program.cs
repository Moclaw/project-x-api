using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using project_x_ba.Services;
using project_x_da.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = $"server={builder.Configuration.GetValue<string>("DBConnect:Server")};uid={builder.Configuration.GetValue<string>("DBConnect:Uid")};pwd={builder.Configuration.GetValue<string>("DBConnect:Pwd")};database={builder.Configuration.GetValue<string>("DBConnect:Database")};CHARSET=utf8;Allow User Variables=True;convert zero datetime=True";


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               var signingKey = Convert.FromBase64String(builder.Configuration["Jwt:SignKey"]);
               var encryptKey = Convert.FromBase64String(builder.Configuration["Jwt:EncryptKey"]);
               string validIssusers = builder.Configuration.GetValue<string>("Jwt:ValidIssuer");
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = false,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = validIssusers,
                   IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                   TokenDecryptionKey = new SymmetricSecurityKey(encryptKey),
                   RequireExpirationTime = true,
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero
               };
           });

builder.Services.AddTransient<UserServices>();

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
