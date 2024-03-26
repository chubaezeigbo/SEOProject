using SEOProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
//var connectionString = builder.Configuration.GetConnectionString("DbConnection");
services.AddControllers();

services.AddEndpointsApiExplorer();

services.AddCors(p => p.AddPolicy("corsPolicy", build =>
{
	build.WithOrigins("*")
    .AllowAnyOrigin()
	.AllowCredentials()
    .AllowAnyMethod()
	.AllowAnyHeader();
}));

services.AddTransient<IBingSearchService, BingSearchService>();
services.AddTransient<IGoogleSearchService, GoogleSearchService>();
services.AddTransient<ISearchHistoryService, SearchHistoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
