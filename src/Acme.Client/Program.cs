using Acme.Core.Data;
using Acme.Core.Interfaces;
using Acme.Core.Persistence;
using Acme.Core.Services;
using Acme.Web.Components;
using Acme.Web.Interfaces;
using Acme.Web.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ISubmissionApiService, SubmissionApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5093/"); 
});

// For controllers + API endpoints
builder.Services.AddControllers(); 
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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
