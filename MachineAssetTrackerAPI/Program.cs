using MachineAssetTrackerAPI.Data;
using MachineAssetTrackerAPI.Interfaces;
using MachineAssetTrackerAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSingleton<MachineAssetData>();
builder.Services.AddHostedService<DataLoader>();
builder.Services.AddScoped<IMachineAssetsService, MachineAssetsService>();
builder.Services.AddScoped<IMachineService, MachineService>();
builder.Services.AddScoped<IAssetService, AssetService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.WebHost.UseUrls("http://0.0.0.0:8080");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix= string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
