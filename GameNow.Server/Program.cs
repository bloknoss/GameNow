using GameNow.Database;
using GameNow.Domain.Entities;
using GameNow.Domain.Interfaces;
using GameNow.Infrastructure.Repositories;
using GameNow.Server.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

internal class Program
{
	private static void Main(string[] args)
	{
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

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.MapFallbackToFile("/index.html");

		app.Run();
	}
}