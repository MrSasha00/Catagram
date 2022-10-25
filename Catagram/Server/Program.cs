using Catagram.Server;
using Catagram.Server.Data;
using Catagram.Server.Models;
using Catagram.Server.Models.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
		.UseSnakeCaseNamingConvention();
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.HttpOnly = false;
	options.Events.OnRedirectToLogin = context =>
	{
		context.Response.StatusCode = 401;
		return Task.CompletedTask;
	};
});
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFtpService>(s => new FtpService(
	new FtpConfigOptions
	{
		ConnectionString = builder.Configuration.GetValue<string>("FtpSettings:DefaultConnection"),
		Login = builder.Configuration.GetValue<string>("FtpSettings:Login"),
		Password = builder.Configuration.GetValue<string>("FtpSettings:Password")
	}));

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();