using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PokedexV2.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PokedexV2Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("PokedexV2Context") ?? throw new InvalidOperationException("Connection string 'PokedexV2Context' not found.")).EnableSensitiveDataLogging());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else {
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
