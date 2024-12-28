using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange:true);
builder.Services.AddOcelot().AddCacheManager(x => x.WithDictionaryHandle());
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

app.UseCors();
app.UseHttpsRedirection();
app.Run();

