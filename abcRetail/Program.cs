using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<abcRetail.Models.DBContext>(
       options => options.UseSqlServer(builder.Configuration.GetConnectionString("abcRetailersDB")));


builder.Services.AddSwaggerGen();
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["BlobStorage:abcRetailersBlob:blobServiceUri"]!).WithName("BlobStorage:abcRetailersBlob");
    clientBuilder.AddQueueServiceClient(builder.Configuration["BlobStorage:abcRetailersBlob:queueServiceUri"]!).WithName("BlobStorage:abcRetailersBlob");
    clientBuilder.AddTableServiceClient(builder.Configuration["BlobStorage:abcRetailersBlob:tableServiceUri"]!).WithName("BlobStorage:abcRetailersBlob");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
