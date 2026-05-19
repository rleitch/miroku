using Microsoft.EntityFrameworkCore;
using Miroku.Data;
using Miroku.Ollama.Extensions;
using Miroku.Web.Components;
using Miroku.Web.Configuration;
using Miroku.Web.Services;

var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration.GetSection("Settings").Get<Settings>()
    ?? throw new InvalidOperationException("Settings section not found in appsettings.json");

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContextFactory<MirokuContext>(options => {
    options.UseSqlServer(
        connectionString,
        sqlOptions => sqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null)
    );
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddOllamaClient(settings.OllamaSettings)
    .AddHttpContextAccessor()
    .AddScoped<UserService>()
    .AddScoped<ConversationService>()
    .AddScoped<CookieService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
