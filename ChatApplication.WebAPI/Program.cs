using ChatApplication.Infrastructure.Hubs;
using ChatApplication.WebAPI.StartupExtensions;
using DotNetEnv;
Env.Load();
var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices(builder.Configuration);


var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHub<ChatHub>("/chathub");

app.Run();
