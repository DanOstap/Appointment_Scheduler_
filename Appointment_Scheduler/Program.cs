using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Http.HttpResults;
using Appointment_Scheduler.Controllers;
using Appointment_Scheduler.Models;
using Appointment_Scheduler.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Core.Configuration;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string connString = builder.Configuration.GetConnectionString("MongoDBConnection");
const string connectionUri = "mongodb://localhost:27017";
var settings = MongoClientSettings.FromConnectionString(connectionUri);

builder.Services.AddSingleton<IMongoClient>(new MongoClient(connString));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
var app = builder.Build();
app.UseDeveloperExceptionPage();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
///   Connection to MongoDB

settings.ServerApi = new ServerApi(ServerApiVersion.V1);

var client = new MongoClient(settings);
try
{
    var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
    Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.UseEndpoints(endpoints =>{  endpoints.MapControllers();   });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=FirstGet}");
app.Run();
