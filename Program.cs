using Microsoft.AspNetCore.HttpOverrides;
using Num2Words.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();



builder.Services.AddControllersWithViews();
    //.AddJsonOptions(options => {
    //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    //    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    //    //options.JsonSerializerOptions.IgnoreNullValues = true;
    //});


builder.Services.AddMvc();
//.ConfigureApiBehaviorOptions(options =>
//{
//    options.SuppressInferBindingSourcesForParameters = true;
//});
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = false;
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors",
        builder => builder
        .WithOrigins("http://localhost:3000")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddSingleton<INumConvSvc, NumConvSvc>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
