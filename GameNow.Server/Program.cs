using GameNow.Database;
using GameNow.Domain.Entities;
using GameNow.Domain.Interfaces;
using GameNow.Infrastructure.Repositories;
using GameNow.Server.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
	options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<GameNowContext>().AddDefaultTokenProviders();



builder.Services.AddScoped<IRepository<Game>, GameRepository>();
builder.Services.AddScoped<IRepository<IdentityUser>, UserRepository>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidateActor = true,
		ValidateIssuer = true,
		ValidateAudience = true,
		RequireExpirationTime = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
		ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
	};
});

builder.Services.AddDbContext<GameNowContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("GameNowConnection"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<GameNowContext>();
	context.Database.Migrate();
}

app.UseCors(options =>
{
	options.
		AllowAnyHeader().
		AllowAnyMethod().
		AllowCredentials();
});


app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
